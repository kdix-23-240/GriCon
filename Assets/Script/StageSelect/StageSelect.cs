using System.Drawing.Drawing2D;
using UnityEngine;
public class StageSelect : MonoBehaviour
{
    [SerializeField] private Camera stageSelectCamera;
    [SerializeField] private float bendWall = 4f; // 曲げセンサーの閾値
    private bool isGrip = false; 
    [SerializeField] private float selectTime = 1f; // ステージ選択のための時間閾値
    private float flameCounter = 0f; // フレームカウンター
    private string stageName = null; // ステージ名を格納する変数
    private TimerModel timerModel; // TimerModelのインスタンス

    void Awake()
    {
        timerModel = new TimerModel(); // TimerModelのインスタンスを生成
    }

    private void Start()
    {
    }

    void Update()
    {
        if (CheckGrip())
        {

            if (flameCounter >= selectTime)
            {
                Debug.Log("ステージを決定");
                int stageNum = DecideStage(); // ステージ番号を決定
                if (stageNum >= 0) // 有効なステージ番号が決定された場合
                {
                    ChangeScene(stageNum); // シーンを変更
                }
                else
                {
                    Debug.LogWarning("無効なステージ番号が選択されました。");
                }
            }   
        }
    }

    /// <summary>
    /// カメラが向いている方向にあるオブジェクトの名前を数字として取得する
    /// それをステージ番号として使用する
    /// 最後にこのステージ番号を返す
    /// </summary>
    /// <returns></returns>

    private int DecideStage()
    {
        int stageNum = -1; // 初期値として無効な値を設定
        RaycastHit hit;
        Ray ray = new Ray(stageSelectCamera.transform.position, stageSelectCamera.transform.forward);
        if(Physics.Raycast(ray, out hit, 100f)) // レイキャストでオブジェクトを検出
        {
            Debug.Log("ヒットしたオブジェクト: " + hit.collider.gameObject.name);
            if (int.TryParse(hit.collider.gameObject.name, out stageNum)) // オブジェクトの名前を整数に変換
            {
                Debug.Log("ステージ番号: " + stageNum);
            }
            else
            {
                Debug.LogWarning("オブジェクトの名前が整数に変換できませんでした。");
                stageNum = -1; // 無効な値を設定
            }
        }
        else
        {
            Debug.LogWarning("レイキャストでオブジェクトがヒットしませんでした。");
        }
        return stageNum;
    }

    private void ChangeScene(int stageNum)
    {
        // シーンを変更する処理をここに実装
        Debug.Log("シーンを変更: " + stageNum);
        stageName = "Stage" + stageNum.ToString(); // ステージ名を設定
        GriConDirectionSetting.stageName = stageName; // GriConDirectionSettingにステージ名を渡す
        UnityEngine.SceneManagement.SceneManager.LoadScene("GriConSetting");
    }

    private bool CheckGrip()
    {
        if(isGrip)
        {
            Debug.LogWarning("握っている");
            return true;
        }
        else
        {
            Debug.Log("握っていない");
            return false;
        }   
    }
}