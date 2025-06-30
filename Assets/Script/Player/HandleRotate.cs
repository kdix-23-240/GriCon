using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// プレイヤーまたはオブジェクトをX/Y/Z軸方向に傾けるスクリプト
/// シリアル通信から得た角度データに基づいて、姿勢を更新する
/// </summary>
public class HandleRotate : MonoBehaviour
{
    [SerializeField] private float offsetRotateX = 0f; // ピッチのオフセット（視覚補正用）
    [SerializeField] private float offsetRotateY = 0f; // ヨーのオフセット（視覚補正用）
    [SerializeField] private float offsetRotateZ = 0f; // ロールのオフセット（視覚補正用）

    private float currentX = 0f;
    private float currentY = 0f;
    private float currentZ = 0f;

    public void RotateX(float rotateX)
    {
        currentX = rotateX + offsetRotateX;
        ApplyRotation();
        Debug.Log($"RotateX: {currentX} (offset: {offsetRotateX})");
    }

    public void RotateY(float rotateY)
    {
        currentY = rotateY + offsetRotateY;
        ApplyRotation();
        Debug.Log($"RotateY: {currentY} (offset: {offsetRotateY})");
    }

    public void RotateZ(float rotateZ)
    {
        currentZ = rotateZ + offsetRotateZ;
        ApplyRotation();
        Debug.Log($"RotateZ: {currentZ} (offset: {offsetRotateZ})");
    }

    private void ApplyRotation()
    {
        // X:ピッチ, Y:ヨー, Z:ロール
        transform.localRotation = Quaternion.Euler(currentX, currentY, currentZ);
    }
}