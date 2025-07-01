using UnityEngine;

public class GripTimer : MonoBehaviour, IGripAction
{
    [SerializeField] private TimerPresenter timerPresenter; // TimertimerPresenter to manage the timer
    [SerializeField] private float gripDuration = 2f; // Duration in seconds

    void Awake()
    {

    }
    void Start()
    {
        if (timerPresenter == null)
        {
            Debug.LogError("GripTimer:タイマーオブジェクトがアタッチされていません    ");
        }
    }
    public void OnGrip(float bend, float bendWall)
    {
        if (bendWall < bend)
        {
            timerPresenter.StartTimer(gripDuration);
        }
        else
        {
            timerPresenter.ResetTimer();
        }
    }
}