using UnityEngine;

public class GripMove : MonoBehaviour, IGripAction
{
    public void OnGrip()
    {

    }

    public void ExitGrip()
    {
        // Implement the logic for exiting the grip here
        Debug.Log("Grip exited");
    }
}