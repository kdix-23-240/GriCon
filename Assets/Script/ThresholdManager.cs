using UnityEngine;

public class ThresholdManager : MonoBehaviour
{
    [Header("曲げ閾値設定 (ScriptableObject)")]
    public ThresholdSettings thresholdSettings;

    // 必要に応じて、インスペクターで値を表示・編集
    // まとめてボタン用 曲げ閾値 (参照)
    public float[] buttonThresholds => thresholdSettings != null ? thresholdSettings.buttonThresholds : null;

    // ゲーム中の移動用 曲げ閾値 (参照)
    public float[] moveThresholds => thresholdSettings != null ? thresholdSettings.moveThresholds : null;
}
