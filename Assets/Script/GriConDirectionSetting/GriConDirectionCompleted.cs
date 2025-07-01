using JetBrains.Annotations;
using UnityEngine;

public class GriConDirectiojnCompleted : MonoBehaviour, ITimerCompletedAction
{
	public void OnTimerCompleted()
	{
		Debug.Log("全方向の設定が完了しました。シーンを変更します。");
		UnityEngine.SceneManagement.SceneManager.LoadScene(StageName.GetInstance().StageNameText);
	}
}