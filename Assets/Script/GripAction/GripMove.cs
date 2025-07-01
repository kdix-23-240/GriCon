using UnityEngine;

public class GripMove : MonoBehaviour, IGripAction
{
    [SerializeField] private GameObject circleHandle;         // 進行方向の基準となるハンドルオブジェクト
    [SerializeField] private float firstPlayerPositionX;      // 初期位置X
    [SerializeField] private float firstPlayerPositionY;      // 初期位置Y
    [SerializeField] private float firstPlayerPositionZ;      // 初期位置Z
    [SerializeField] private float speedRate = 1;             // 速度調節のための定数

    public void OnGrip(float bend)
    {
        Move(bend);
    }

    public void ExitGrip()
    {
        // Implement the logic for exiting the grip here
        //Debug.Log("Grip exited");
    }

    /// <summary>
    /// ハンドルの向き（下方向）に対してプレイヤーを移動させる
    /// </summary>
    private void Move(float moveSpeed)
    {
        Vector3 moveDirection = -circleHandle.transform.up; // ハンドルの下向きを移動方向に設定
        transform.Translate(moveDirection.normalized * moveSpeed * speedRate, Space.World);
    }

    /// <summary>
    /// プレイヤーの位置と角度を初期状態にリセットする
    /// </summary>
    public void ResetPlayerPosition()
    {
        transform.position = new Vector3(firstPlayerPositionX, firstPlayerPositionY, firstPlayerPositionZ); // 初期位置に戻す
        transform.rotation = Quaternion.Euler(0, 0, 0); // 回転も初期化
        GameSystem.isReset = false; // リセットフラグを解除
    }
}