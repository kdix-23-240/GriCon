using Unity.VisualScripting.FullSerializer;
using UnityEngine;
using System.Collections;

public class WarningSender : MonoBehaviour
{
    private Coroutine warningCoroutine;
    private bool isWarningSequenceRunning = false;
    private float bilibiliTime = 2f; // 警告間の待機時間（秒）

    public void OnChangeWarningLevel(int level)
    {
        switch (level)
        {
            case 1:
                //Debug.Log("---------------------------------------------------");
                //Debug.Log("WarningSender:WarningLevel3 を検出");
                //Debug.Log("WarningSender:棒に近づきすぎです、立て直してください");
                //Debug.Log("---------------------------------------------------");
                SendWarning('1');
                break;
            case 2:
                //Debug.Log("---------------------------------------------------");
                //Debug.Log("WarningSender:WarningLevel2 を検出");
                //Debug.Log("WarningSender:棒に近づきました、気を付けてください");
                //Debug.Log("---------------------------------------------------");
                SendWarning('2');
                break;
            case 3:
                //Debug.Log("---------------------------------------------------");
                //Debug.Log("WarningSender:WarningLevel1 を検出");
                //Debug.Log("WarningSender:棒に少し近づきました");
                //Debug.Log("---------------------------------------------------");
                SendWarning('3');
                break;
            case 4:
                //Debug.Log("---------------------------------------------------");
                //Debug.Log("WarningSender:WarningLevel4 を検出");
                //Debug.Log("WarningSender:棒に触れました、ゲームオーバーです");
                //Debug.Log("---------------------------------------------------");
                StartWarningSequence();
                break;
            case 5:
                //    Debug.Log("---------------------------------------------------");
                //    Debug.Log("WarningSender:安全な状態に戻りました");
                //    Debug.Log("---------------------------------------------------");
                SendWarning('5');
                break;
            default:
                //    Debug.Log("---------------------------------------------------");
                //    Debug.LogWarning("WarningSender:不正な値の警告です");
                //    Debug.LogWarning("[WarningSender] Invalid warning level: " + level);
                //    Debug.Log("---------------------------------------------------");
                break;
        }
    }
    /// <summary>
    /// 衝突時などに呼び出し、警告4→5を段階的に送信する
    /// </summary>
    private void StartWarningSequence()
    {
        if (!isWarningSequenceRunning)
        {
            if (warningCoroutine != null)
                StopCoroutine(warningCoroutine);

            warningCoroutine = StartCoroutine(WarningSequenceCoroutine());
        }
    }

    private IEnumerator WarningSequenceCoroutine()
    {
        isWarningSequenceRunning = true;

        SendWarning('4');
        //Debug.Log("[WarningManager] Sent '4'");

        yield return new WaitForSeconds(bilibiliTime);

        SendWarning('5');
        //Debug.Log("[WarningManager] Sent '5'");

        isWarningSequenceRunning = false;
    }

    private void SendWarning(char levelChar)
    {
        if (Get_Information.Instance != null)
        {
            Get_Information.Instance.SetOutgoingByte((byte)levelChar);
            //Debug.Log($"[WarningManager] Sent warning level command: '{levelChar}'");
        }
    }
}