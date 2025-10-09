using Unity.VisualScripting.FullSerializer;
using UnityEngine;
using System.Collections;

public class WarningSender : MonoBehaviour
{
    private Coroutine warningCoroutine;
    private bool isWarningSequenceRunning = false;
    private float bilibiliTime = 2f; // �x���Ԃ̑ҋ@���ԁi�b�j

    public void OnChangeWarningLevel(int level)
    {
        switch (level)
        {
            case 1:
                SendWarning('1');
                break;
            case 2:
                SendWarning('2');
                break;
            case 3:
                SendWarning('3');
                break;
            case 4:
                StartWarningSequence();
                break;
            case 5:
                SendWarning('5');
                break;
            default:
                break;
        }
    }
    /// <summary>
    /// �Փˎ��ȂǂɌĂяo���A�x��4��5��i�K�I�ɑ��M����
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