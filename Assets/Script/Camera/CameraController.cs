using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// カメラの操作を制御する
/// プレイヤーカメラのズームイン、ズームアウト、リセットを行う
/// </summary>
public class CameraController : MonoBehaviour
{
    private float cameraPositionX = 0;
    private float cameraPositionY = 0;
    private float cameraPositionZ = 0;
    private float parentPositionX = 0;
    private float parentPositionY = 0;
    private float parentPositionZ = 0;
    [SerializeField] private float moveSpeed;
    [SerializeField] private float rotationSpeedX;
    [SerializeField] private float positionLimitX;
    [SerializeField] private float rotationLimitX;
    [SerializeField] private float zoomInLimitCount;
    [SerializeField] private float zoomOutLimitCount;
    private float newXPosition;
    private float newYRotation;
    private int clickCount = 0;
    private GripMove playerMove;

    void Start()
    {
        cameraPositionX = transform.position.x;
        cameraPositionY = transform.position.y;
        cameraPositionZ = transform.position.z;

        parentPositionX = transform.parent.position.x;
        parentPositionY = transform.parent.position.y;
        parentPositionZ = transform.parent.position.z;
    }

    void Update()
    {
        // カメラが常に親オブジェクトを向くようにする
        if (transform.parent != null)
        {
            transform.LookAt(transform.parent.position);
        }
        // DelayMove();
    }

    private void DelayMove()
    {
        RegisterCameraPosition();
        if (Input.GetKey(KeyCode.A))
        {
            DelayMovePlus();
        }

        if (Input.GetKey(KeyCode.D))
        {
            DelayMoveMinus();
        }
    }

    /// <summary>
    /// プレイヤーが左に動いた時にカメラを右に動かす
    /// </summary>
    private void DelayMovePlus()
    {
        newXPosition = transform.position.x + moveSpeed;
        if(-positionLimitX < (transform.parent.position.x - transform.position.x))
        {
            transform.position = new Vector3(newXPosition, transform.position.y, transform.position.z);
        }
        else
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        }
    }

    /// <summary>
    /// プレイヤーが右に動いた時にカメラを左に動かす
    /// </summary>
    private void DelayMoveMinus()
    {
        newXPosition = transform.position.x - moveSpeed;
        if ((transform.parent.position.x - transform.position.x) < positionLimitX)
        {
            transform.position = new Vector3(newXPosition, transform.position.y, transform.position.z);
        }
        else
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        }
    }

    public void ZoomIn()
    {
        RegisterCameraPosition();
        if (-zoomOutLimitCount <= clickCount && clickCount < zoomInLimitCount){
            transform.position = new Vector3(cameraPositionX, cameraPositionY, cameraPositionZ + 0.001f);
            clickCount++;
        }
        Debug.Log(clickCount);
    }

    public void ZoomOut()
    {
        RegisterCameraPosition();
        if (-zoomOutLimitCount < clickCount && clickCount <= zoomInLimitCount){
            transform.position = new Vector3(cameraPositionX, cameraPositionY, cameraPositionZ - 0.001f);
            clickCount--;
        }
        Debug.Log(clickCount);
    }

    public void Reset()
    {
        RegisterParentPosition();
        transform.position = new Vector3(parentPositionX, parentPositionY + 0.1f, parentPositionZ - 0.6f);
        clickCount = 0;
    }

    /// <summary>
    /// カメラのその瞬間の位置を登録する
    /// </summary>
    private void RegisterCameraPosition()
    {
        cameraPositionX = transform.position.x;
        cameraPositionY = transform.position.y;
        cameraPositionZ = transform.position.z;
    }

    /// <summary>
    /// 親オブジェクトのその瞬間の位置を登録する
    /// </summary>
    private void RegisterParentPosition()
    {
        parentPositionX = transform.parent.position.x;
        parentPositionY = transform.parent.position.y;
        parentPositionZ = transform.parent.position.z;
    }
}