using UnityEngine;

public class UnCover : MonoBehaviour
{
    [SerializeField] private float distance = 10f;
    private bool isUncovered = false;

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("Uncovering/covering the object.");
            Vector3 pos = transform.position;
            if(isUncovered)
            {
                pos.y -= distance;
            }
            else
            {
                pos.y += distance;
            }
            transform.position = pos;
            isUncovered = !isUncovered;
        }
    }
}