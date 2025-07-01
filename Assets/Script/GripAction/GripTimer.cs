using UnityEngine;

public class GripTimer : MonoBehaviour, IGripAction
{
    [SerializeField] private TimerPresenter timerPresenter; // TimertimerPresenter to manage the timer

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
    public void OnGrip(float bend)
    {
        timerPresenter.StartTimer(bend); // Start the timer when grip is detected
    }
    public void ExitGrip()
    {
        timerPresenter.ResetTimer(); // Reset the timer
    }
}