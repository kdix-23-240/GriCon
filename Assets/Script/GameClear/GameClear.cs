using UnityEngine;

public class GameClear : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) // プレイヤーがトリガーに入ったとき
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene("GameClear");
        }
    }
}