using UnityEngine;

public class NoAction : MonoBehaviour, IGripAction
{
    public void OnGrip()
    {
        // Grip action does nothing
    }

    public void ExitGrip()
    {
        // Exit grip action does nothing
    }
}