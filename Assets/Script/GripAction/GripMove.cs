using UnityEngine;

public class GripMove : MonoBehaviour, IGripAction
{
    public void OnGrip(float bend)
    {

    }

    public void ExitGrip()
    {
        // Implement the logic for exiting the grip here
        Debug.Log("Grip exited");
    }
}