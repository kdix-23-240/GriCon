using UnityEngine;

public class NoAction : MonoBehaviour, IGripAction
{
    public void OnGrip(float bend)
    {
        // Grip action does nothing
    }

    public void ExitGrip()
    {
        // Exit grip action does nothing
    }
}