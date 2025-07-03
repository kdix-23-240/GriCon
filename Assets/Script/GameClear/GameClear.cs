using UnityEngine;

public class GameClear : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "PlayerCollision")
        {
            Debug.Log("Game Clear!"); // ゲームクリアのメッセージをログに表示
            UnityEngine.SceneManagement.SceneManager.LoadScene("GameClear");
        }
    }
}