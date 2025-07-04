using UnityEngine;

public class CameraMove : MonoBehaviour
{
    [SerializeField] private float speed = 5f;

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.UpArrow))
        {
            transform.position += new Vector3(0, 0, speed);
        }

        if(Input.GetKeyDown(KeyCode.DownArrow))
        {
            transform.position += new Vector3(0, 0, -speed);
        }
    }
}