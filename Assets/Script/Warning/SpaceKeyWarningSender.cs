using UnityEngine;

// スペースキーが押されたときに '6' をシリアルへ送信するシンプルな送信スクリプト
// 参照: WarningSender / Get_Information.SetOutgoingByte(byte)
public class SpaceKeyWarningSender : MonoBehaviour
{
    [Header("Send Settings")]
    [Tooltip("送信する文字（既定: '6'）")]
    [SerializeField] private char sendChar = '6';

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            var gi = Get_Information.Instance;
            if (gi != null)
            {
                gi.SetOutgoingByte((byte)sendChar);
                Debug.Log($"[SpaceKeyWarningSender] Sent '{sendChar}' by Space key.");
            }
            else
            {
                Debug.LogWarning("[SpaceKeyWarningSender] Get_Information.Instance が見つかりません");
            }
        }
    }
}
