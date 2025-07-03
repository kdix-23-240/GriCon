using UnityEngine;

public class GripTimer : MonoBehaviour, IGripAction
{
    [SerializeField] private TimerPresenter timerPresenter;
    [SerializeField] private float duration = 2f; // 何秒数えるか
    [SerializeField] private GameObject gripAnimationObject; // グリップアニメーションのオブジェクト

    void Start()
    {
        if (timerPresenter == null)
        {
            Debug.LogError("GripTimer:タイマーオブジェクトがアタッチされていません    ");
        }
    }

    public void OnGrip(float bend)
    {
        timerPresenter.StartTimer(duration); // 時間計測開始
        gripAnimationObject.GetComponent<GripAnimation>().Show(); // グリップアニメーションを表示
    }

    public void ExitGrip()
    {
        timerPresenter.ResetTimer(); // 時間計測終了、リセット
        gripAnimationObject.GetComponent<GripAnimation>().Hide(); // グリップアニメーションを非表示
    }
}