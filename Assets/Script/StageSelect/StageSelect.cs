using UnityEngine;
public class StageSelect : MonoBehaviour, ITimerCompletedAction
{
    [SerializeField] private Camera stageSelectCamera;
    [SerializeField] private SelectSE selectSE; // SEを再生するためのコンポーネント

    public void OnTimerCompleted()
    {
        ChangeSceneWithSE(); // タイマー完了時にSEを再生
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

        if (stageName == "Retry")
        {
            Debug.Log("選択されたステージ名: " + StageName.GetInstance().StageNameText);
            UnityEngine.SceneManagement.SceneManager.LoadScene("GriConSetting");
            return; // "Retry"の場合はステージ名を変更しない
        }

        if (stageName == "NextStage")
        {
            Debug.Log("選択されたステージ名: " + StageName.GetInstance().StageNameText);
            
            switch (StageName.GetInstance().StageNameText)
            {
                case "Stage1":
                    stageName = "Stage2"; // Stage1からNextStageを選択した場合はStage2に変更
                    break;
                case "Stage2":
                    stageName = "Stage3"; // Stage2からNextStageを選択した場合はStage3に変更
                    break;
                case "Stage3":
                    stageName = "Stage1"; // Stage3からNextStageを選択した場合はStage4に変更
                    break;
                default:
                    Debug.LogWarning("次のステージが設定されていません。");
                    break;
            }
        }

        if(stageName == "StageSelect")
        {
            Debug.Log("選択されたステージ名: " + StageName.GetInstance().StageNameText);
            UnityEngine.SceneManagement.SceneManager.LoadScene(stageName);
            return; // "Stage4"の場合はステージ名を変更しない
        }

        // シーンを変更する処理をここに実装
        Debug.Log("シーンを変更: " + stageName);
        StageName.GetInstance().StageNameText = stageName; // ステージ名を設定
        Debug.Log("選択されたステージ名: " + StageName.GetInstance().StageNameText);
        UnityEngine.SceneManagement.SceneManager.LoadScene("GriConSetting");
    }

    private async void ChangeSceneWithSE()
    {
        selectSE.PlaySelectSE(); // SEを再生
        await System.Threading.Tasks.Task.Delay(1000); // 1秒待機（SEの再生時間に合わせる
        ChangeScene(DecideStage()); // タイマー完了時にステージを決定し、シーンを変更
    }
}