using UnityEngine;

public class ReturnFromSecret : MonoBehaviour
{
    public void OnClick()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("StageSelect");
    }
}
