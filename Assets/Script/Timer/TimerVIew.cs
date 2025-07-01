using UnityEngine;
using UnityEngine.UI;

public class TimerView : MonoBehaviour
{
    [SerializeField] private Text timerText; // タイマー表示用のTextコンポーネント

    public void UpdateTime(float elapsedTime)
    {
        // タイマーの経過時間を表示
        timerText.text = $"{elapsedTime:F2}";
    }
}