// Unity用ヘッダー同期付き・整数角度対応シリアル受信スクリプト
// 改良点 (2025-10-09):
//  - ポート名/ボーレートを Inspector から設定可能に
//  - タイムアウト時のログをレート制限（スパム防止）
//  - 再接続ロジック（接続喪失時に一定間隔で再試行）
//  - 読み取りタイムアウト短縮＋ヘッダー同期を非ブロッキング化
//  - Dispose 安全性とアプリ終了時の明示的クリーンアップ
using UnityEngine;
using System;
using System.IO.Ports;
using System.Threading;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class Get_Information : MonoBehaviour
{
    public static Get_Information Instance { get; private set; } // Singletonインスタンス

    [Header("Serial Port Settings")]
    [Tooltip("使用するシリアルポート (例: COM3)。空の場合は最初に見つかったポートを自動選択。")]
    [SerializeField] private string portName = "COM9";
    [Tooltip("通信速度 (ボーレート)")]
    [SerializeField] private int baudRate = 115200;
    [Tooltip("ReadTimeout (ms)。短くしてループに戻り再接続や終了判定を素早くする。")]
    [SerializeField] private int readTimeoutMs = 200;
    [Tooltip("WriteTimeout (ms)")]
    [SerializeField] private int writeTimeoutMs = 500;
    [Tooltip("タイムアウトや再接続などの警告を出す最小間隔 (秒)")]
    [SerializeField] private float warnLogInterval = 5f;
    [Tooltip("シリアル切断検出後の再接続試行間隔 (秒)")]
    [SerializeField] private float reconnectInterval = 3f;

    private SerialPort serial;        // シリアルポートインスタンス
    private Thread readThread;        // データ受信用スレッド
    private volatile bool isRunning = false;  // 受信スレッドの制御フラグ
    private volatile bool requestReconnect = false;
    private DateTime lastWarnLogTime = DateTime.MinValue;
    private DateTime lastReconnectAttempt = DateTime.MinValue;

    public float[] receivedData = new float[4]; // 受信データ：pitch, roll, yaw, bend

    private const int messageSize = 8;         // データ長（int16が4つで8バイト）
    private byte[] buffer = new byte[messageSize]; // バッファ配列

    // Awakeはインスタンス生成時に最初に呼ばれる
    void Awake()
    {
        // Singleton初期化（複数生成防止）
        if (Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(this.gameObject); // シーン遷移でも破棄しない
    }

    // Startはゲーム開始時に一度だけ呼び出される初期化処理
    void Start()
    {
        OpenPort();
    }

    private void OpenPort()
    {
        if (serial != null && serial.IsOpen) return;

        // ポート自動選択
        if (string.IsNullOrWhiteSpace(portName))
        {
            string[] ports = SerialPort.GetPortNames();
            if (ports.Length > 0)
            {
                portName = ports[0];
                Debug.Log("[Serial] Auto-selected port: " + portName);
            }
            else
            {
                RateLimitedWarn("[Serial] 利用可能なシリアルポートが見つかりません");
                ScheduleReconnect();
                return;
            }
        }

        serial = new SerialPort(portName, baudRate)
        {
            ReadTimeout = readTimeoutMs,
            WriteTimeout = writeTimeoutMs,
            DtrEnable = true
        };

        try
        {
            serial.Open();
            if (!isRunning)
            {
                isRunning = true;
                readThread = new Thread(ReadSerialData) { IsBackground = true, Name = "SerialReadThread" };
                readThread.Start();
            }
            requestReconnect = false;
            Debug.Log("[Serial] Opened " + portName + " @" + baudRate);
        }
        catch (Exception e)
        {
            RateLimitedWarn("[Serial] ポート接続失敗 (" + portName + "): " + e.Message);
            ScheduleReconnect();
        }
    }

    // オブジェクト破棄時に呼ばれるクリーンアップ処理
    void OnDestroy()
    {
        Shutdown();
    }

    void OnApplicationQuit()
    {
        Shutdown();
    }

    private void Shutdown()
    {
        isRunning = false;
        try
        {
            if (readThread != null && readThread.IsAlive)
            {
                if (!readThread.Join(500))
                {
                    readThread.Interrupt();
                }
            }
        }
        catch { /* ignore */ }

        if (serial != null)
        {
            try
            {
                if (serial.IsOpen) serial.Close();
            }
            catch { }
            finally
            {
                serial.Dispose();
            }
            serial = null;
        }
    }

    // シリアルデータを非同期に読み取る処理（ヘッダー同期付き）
    private void ReadSerialData()
    {
        while (isRunning)
        {
            if (serial == null || !serial.IsOpen)
            {
                if (requestReconnect && (DateTime.UtcNow - lastReconnectAttempt).TotalSeconds >= reconnectInterval)
                {
                    lastReconnectAttempt = DateTime.UtcNow;
                    OpenPort();
                }
                Thread.Sleep(50);
                continue;
            }

            try
            {
                // ヘッダー同期 (非ブロッキングループ)。ReadByte がタイムアウトしたら再ループ。
                int headerByte = -1;
                while (isRunning && serial.IsOpen)
                {
                    try
                    {
                        headerByte = serial.ReadByte();
                        if (headerByte == 'S') break;
                    }
                    catch (TimeoutException)
                    {
                        // continue to next loop iteration without spamming logs
                        continue;
                    }
                }
                if (!isRunning) break;
                if (headerByte != 'S') continue; // 何らかの理由で抜けた

                int bytesRead = 0;
                while (bytesRead < messageSize && isRunning && serial.IsOpen)
                {
                    try
                    {
                        int r = serial.Read(buffer, bytesRead, messageSize - bytesRead);
                        if (r > 0) bytesRead += r; // r==0 は ReadTimeout の可能性
                    }
                    catch (TimeoutException)
                    {
                        // 再試行
                        continue;
                    }
                }
                if (bytesRead < messageSize) continue; // 不完全 → 破棄

                // 受信データ変換
                for (int i = 0; i < 3; i++)
                {
                    short raw = BitConverter.ToInt16(buffer, i * 2);
                    receivedData[i] = raw / 10.0f;
                }
                short bendRaw = BitConverter.ToInt16(buffer, 6);
                receivedData[3] = bendRaw / 10.0f;
            }
            catch (TimeoutException)
            {
                // 無視（ループ継続）。ログレート制限で出したい場合は以下：
                RateLimitedWarn("[Serial] Read timeout");
            }
            catch (Exception e)
            {
                RateLimitedWarn("[Serial] Read error: " + e.Message);
                // 切断判定
                if (!serial.IsOpen)
                {
                    ScheduleReconnect();
                }
            }
        }
    }

    private void ScheduleReconnect()
    {
        requestReconnect = true;
        lastReconnectAttempt = DateTime.UtcNow.AddSeconds(-reconnectInterval); // すぐに試行させる
    }

    private void RateLimitedWarn(string msg)
    {
        if ((DateTime.UtcNow - lastWarnLogTime).TotalSeconds >= warnLogInterval)
        {
            lastWarnLogTime = DateTime.UtcNow;
            Debug.LogWarning(msg);
        }
    }

    // シリアルポートに1バイトのコマンドを送信するメソッド
    public void SetOutgoingByte(byte msg)
    {
        if (serial != null && serial.IsOpen)
        {
            try
            {
                serial.Write(new byte[] { msg }, 0, 1);
                Debug.Log($"[WarningSystem] Sent warning level command: '{(char)msg}'");
            }
            catch (Exception e)
            {
                RateLimitedWarn("[Serial] Write failed: " + e.Message);
                if (!serial.IsOpen) ScheduleReconnect();
            }
        }
        else
        {
            RateLimitedWarn("[Serial] Port not open. Cannot send.");
        }
    }

    // 外部から現在の受信データ（float[4]）を取得するためのゲッター
    public float[] GetReceivedData() => receivedData;

#if UNITY_EDITOR
    [CustomEditor(typeof(Get_Information))]
    private class GetInformationEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();
            var gi = (Get_Information)target;
            EditorGUILayout.Space();
            EditorGUILayout.LabelField("Runtime Status", EditorStyles.boldLabel);
            EditorGUILayout.LabelField("IsOpen", (gi.serial != null && gi.serial.IsOpen).ToString());
            if (GUILayout.Button("Reconnect"))
            {
                gi.ScheduleReconnect();
            }
        }
    }
#endif
}
