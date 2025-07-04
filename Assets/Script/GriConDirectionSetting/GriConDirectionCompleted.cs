using UnityEngine;

/// <summary>
/// コントローラーの角度調整画面で全方向の設定が完了した際に呼び出されるクラス
/// </summary>
public class GriConDirectionCompleted : MonoBehaviour, ITimerCompletedAction
{
	[SerializeField] private SelectSE selectSE;
    public void OnTimerCompleted()
	{
		ChangeSceneWithSE(); // タイマー完了時にシーンを変更
    }

	private async void ChangeSceneWithSE()
	{
		selectSE.PlaySelectSE();
		await System.Threading.Tasks.Task.Delay(1000); // 1秒待機
        Debug.Log("全方向の設定が完了しました。シーンを変更します。");
		Debug.Log("StageName: " + StageName.GetInstance().StageNameText);
        UnityEngine.SceneManagement.SceneManager.LoadScene(StageName.GetInstance().StageNameText);
    }
}