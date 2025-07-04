using UnityEngine;
using System.Collections;

public class SimpleWarningSender : MonoBehaviour
{
    private Coroutine warningCoroutine;
    private bool isWarningSequenceRunning = false;
    private float bilibiliTime = 2f; // 警告間の待機時間（秒）

    public void MaxWarningForce()
    {
        StartWarningSequence();
    }

    public void WarningResetForce()
    {
        SendWarning('5');
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

        yield return new WaitForSeconds(bilibiliTime);

        SendWarning('5');
        isWarningSequenceRunning = false;
    }

    private void SendWarning(char levelChar)
    {
        if (Get_Information.Instance != null)
        {
            Get_Information.Instance.SetOutgoingByte((byte)levelChar);
        }
    }
}