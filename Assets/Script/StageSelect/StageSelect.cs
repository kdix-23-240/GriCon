using System.Drawing.Drawing2D;
using UnityEngine;
public class StageSelect : MonoBehaviour, ITimerCompletedAction
{
    [SerializeField] private Camera stageSelectCamera;

    public void OnTimerCompleted()
    {
        ChangeScene(DecideStage()); // タイマー完了時にステージを決定し、シーンを変更
    }

    /// <summary>
    /// カメラが向いている方向にあるオブジェクトの名前を取得する
    /// それをステージ名として使用する
    /// 最後にこのステージ名を返す
    /// </summary>
    /// <returns></returns>

    private string DecideStage()
    {
        string stageName = null; // 初期値として無効な値を設定
        RaycastHit hit;
        Ray ray = new Ray(stageSelectCamera.transform.position, stageSelectCamera.transform.forward);
        if(Physics.Raycast(ray, out hit, 100f)) // レイキャストでオブジェクトを検出
        {
            Debug.Log("ヒットしたオブジェクト: " + hit.collider.gameObject.name);
            stageName = hit.collider.gameObject.name; // ヒットしたオブジェクトの名前を取得
        }
        else
        {
            Debug.LogWarning("レイキャストでオブジェクトがヒットしませんでした。");
            stageName = null; // デフォルトのステージ名を設定
        }
        return stageName;
    }

    private void ChangeScene(string stageName)
    {
        if (stageName == null)
        {
            Debug.LogWarning("StageSelect:ステージが選ばれていない");
        }
        // シーンを変更する処理をここに実装
        Debug.Log("シーンを変更: " + stageName);
        StageName.GetInstance().StageNameText = stageName; // ステージ名を設定
        Debug.Log("選択されたステージ名: " + StageName.GetInstance().StageNameText);
        UnityEngine.SceneManagement.SceneManager.LoadScene("GriConSetting");
    }
}