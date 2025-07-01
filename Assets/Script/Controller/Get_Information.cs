// Unity用ヘッダー同期付き・整数角度対応シリアル受信スクリプト
using UnityEngine;
using System;
using System.IO.Ports;
using System.Threading;

public class Get_Information : MonoBehaviour
{
    public static Get_Information Instance { get; private set; } // Singletonインスタンス

    [Header("Serial Port Settings")]
    private string portName = "COM4";  // 使用するシリアルポート名（※元はpublicだが、外部設定不要なのでprivateに）
    private int baudRate = 9600;       // 通信速度（ボーレート）

    private SerialPort serial;        // シリアルポートインスタンス
    private Thread readThread;       // データ受信用スレッド
    private volatile bool isRunning = false;  // 受信スレッドの制御フラグ

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
        // シリアルポート初期化
        serial = new SerialPort(portName, baudRate);
        serial.ReadTimeout = 1000;
        serial.WriteTimeout = 1000;
        serial.DtrEnable = true;

        try
        {
            // シリアルポートを開き、受信用スレッドを開始
            serial.Open();
            isRunning = true;
            readThread = new Thread(ReadSerialData);
            readThread.Start();
        }
        catch (Exception e)
        {
            Debug.LogError("Failed to open serial port: " + e.Message);
        }
    }

    // オブジェクト破棄時に呼ばれるクリーンアップ処理
    void OnDestroy()
    {
        // スレッドを停止し、シリアルポートを閉じる
        isRunning = false;
        if (readThread != null && readThread.IsAlive)
            readThread.Join();

        if (serial != null && serial.IsOpen)
            serial.Close();
    }

    // シリアルデータを非同期に読み取る処理（ヘッダー同期付き）
    private void ReadSerialData()
    {
        while (isRunning)
        {
            try
            {
                // 'S'（ヘッダー文字）を受信するまで読み飛ばす
                while (serial.ReadByte() != 'S')
                {
                    if (!isRunning) return;
                }

                // ヘッダー後の8バイトをバッファに読み込む（完全受信までループ）
                int bytesRead = 0;
                while (bytesRead < messageSize)
                {
                    bytesRead += serial.Read(buffer, bytesRead, messageSize - bytesRead);
                }

                // ピッチ・ロール・ヨー（角度）はint16で0.1度単位なのでfloatに変換
                for (int i = 0; i < 3; i++)
                {
                    short raw = BitConverter.ToInt16(buffer, i * 2);
                    receivedData[i] = raw / 10.0f;
                }

            // bend（float値 *10 をint16_tに変換されたもの）を受信
            short bendRaw = BitConverter.ToInt16(buffer, 6);  // 6バイト目から2バイト
            receivedData[3] = bendRaw / 10.0f;  // 0.1刻みで変換
            }
            catch (Exception e)
            {
                Debug.LogWarning("Serial Read Error: " + e.Message);
            }
        }
    }

    // シリアルポートに1バイトのコマンドを送信するメソッド
    public void SetOutgoingByte(byte msg)
    {
        if (serial != null && serial.IsOpen)
        {
            serial.Write(new byte[] { msg }, 0, 1);
            Debug.Log($"[WarningSystem] Sent warning level command: '{(char)msg}'");
        }
    }

    // 外部から現在の受信データ（float[4]）を取得するためのゲッター
    public float[] GetReceivedData()
    {
        return receivedData;
    }
}
