using UnityEngine;

public class GoToSecret : MonoBehaviour
{
    public void OnClick()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("GriConShow");
    }
}