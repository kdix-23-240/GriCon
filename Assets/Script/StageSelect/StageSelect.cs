using System.Drawing.Drawing2D;
using UnityEngine;
public class StageSelect : MonoBehaviour, ITimerCompletedAction
{
    [SerializeField] private Camera stageSelectCamera;
    private string stageName = null; // ステージ名を格納する変数

    public void OnTimerCompleted()
    {
        ChangeScene(DecideStage()); // タイマー完了時にステージを決定し、シーンを変更
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
}