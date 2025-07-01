using UnityEngine;

/// <summary>
/// コントローラーの角度調整画面で全方向の設定が完了した際に呼び出されるクラス
/// </summary>
public class GriConDirectionCompleted : MonoBehaviour, ITimerCompletedAction
{
	public void OnTimerCompleted()
	{
		Debug.Log("全方向の設定が完了しました。シーンを変更します。");
		UnityEngine.SceneManagement.SceneManager.LoadScene(StageName.GetInstance().StageNameText);
	}
}