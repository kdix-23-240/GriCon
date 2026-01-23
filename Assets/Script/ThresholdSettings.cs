using UnityEngine;

[CreateAssetMenu(fileName = "ThresholdSettings", menuName = "Settings/ThresholdSettings")]
public class ThresholdSettings : ScriptableObject
{
    [Header("まとめてボタン用 曲げ閾値")]
    public float[] buttonThresholds;

    [Header("ゲーム中の移動用 曲げ閾値")]
    public float[] moveThresholds;
}
