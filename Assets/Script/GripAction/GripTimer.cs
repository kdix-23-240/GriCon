using UnityEngine;

public class GripTimer : MonoBehaviour, IGripAction
{
    [SerializeField] private TimerPresenter timerPresenter;
    [SerializeField] private float duration = 2f; // 何秒数えるか

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
    }

    public void ExitGrip()
    {
        timerPresenter.ResetTimer(); // 時間計測終了、リセット
    }
}