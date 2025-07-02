using UnityEngine;
/// <summary>
/// プレイヤーまたはオブジェクトをX/Y/Z軸方向に傾けるスクリプト
/// シリアル通信から得た角度データに基づいて、姿勢を更新する
/// </summary>
public class SelectBaseView : MonoBehaviour
{
    //[SerializeField] private float offsetRotateX = 0f; // ピッチのオフセット（視覚補正用）
    //[SerializeField] private float offsetRotateY = 0f; // ヨーのオフセット（視覚補正用）
    [SerializeField] private float offsetRotateZ = 0f; // ロールのオフセット（視覚補正用）

    //private float currentX = 0f;
    //private float currentY = 0f;
    private float currentZ = 0f;

    //public void RotateX(float rotateX)
    //{
    //    currentX = rotateX + offsetRotateX;
    //    ApplyRotation();
    //    //Debug.Log($"RotateX: {currentX} (offset: {offsetRotateX})");
    //}

    //public void RotateY(float rotateY)
    //{
    //    currentY = rotateY + offsetRotateY;
    //    ApplyRotation();
    //    //Debug.Log($"RotateY: {currentY} (offset: {offsetRotateY})");
    //}

    public void RotateZ(float rotateZ)
    {
        currentZ = rotateZ + offsetRotateZ;
        ApplyRotation();
        //Debug.Log($"RotateZ: {currentZ} (offset: {offsetRotateZ})");
    }

    /// <summary>
    /// コントローラーの傾きを反映させる
    /// </summary>
    private void ApplyRotation()
    {
        // 現在のX, Y回転を維持し、Zのみ更新
        Vector3 euler = transform.localRotation.eulerAngles;
        transform.localRotation = Quaternion.Euler(euler.x, euler.y, currentZ);
    }
}