using UnityEngine;

/// <summary>
/// デバッグ用のクラス
/// コントローラーの角度を可視化するために赤い線を描画
/// </summary>
public class CameraLay : MonoBehaviour
{
    void Update()
    {
        // 6方向に向けて赤い線を描画
        Debug.DrawRay(transform.position, transform.forward * 50f, Color.red);
        Debug.DrawRay(transform.position, -transform.forward * 50f, Color.red);
        Debug.DrawRay(transform.position, transform.right * 50f, Color.red);
        Debug.DrawRay(transform.position, -transform.right * 50f, Color.red);
        Debug.DrawRay(transform.position, transform.up * 50f, Color.red);
        Debug.DrawRay(transform.position, -transform.up * 50f, Color.red);
    }
}