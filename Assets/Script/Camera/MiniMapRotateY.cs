using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniMapRotateY : MonoBehaviour
{
    private float rotateSpeed = 0.3f;
    [SerializeField] private Camera miniMapCamera;
    void Start()
    {

    }

    void Update()
    {
        //if (GameSystem.Instance.GetCanRotate())
        //{
        //    RotateHandle();
        //}
        //GazeStage();
    }

    /// <summary>
    /// 持ち手をyz軸に回転させる(持ち手自体がすでに回転させてあるので軸が狂ってる)
    /// </summary>
    private void RotateHandle()
    {
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            transform.RotateAround(transform.parent.position, Vector3.up, rotateSpeed);
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            transform.RotateAround(transform.parent.position, Vector3.up, -rotateSpeed);
        }
    }

    private void GazeStage()
    {
        miniMapCamera.transform.LookAt(transform.parent.position);
        // 見ている方向に赤い線を表示
        // Debug.DrawRay(miniMapCamera.transform.position, miniMapCamera.transform.forward * 10, Color.red);
    }
}