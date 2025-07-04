using System.Threading.Tasks;
using UnityEngine;

public class ResultSceneDelay : MonoBehaviour
{
    [SerializeField] private float delayTime = 2.0f; // 遅延時間を設定

    private void Start()
    {
        StartCoroutine(DelayCoroutine());
    }

    private System.Collections.IEnumerator DelayCoroutine()
    {
        //// 操作不可能状態にする
        //GameSystem.canRotate = false;
        //GameSystem.canGrip = false;
        //GameSystem.canMove = false;

        yield return new WaitForSeconds(delayTime); // 指定した時間だけ待機

        //// 操作可能状態に戻す
        //GameSystem.canRotate = true;
        //GameSystem.canGrip = true;
        //GameSystem.canMove = true;
    }
}