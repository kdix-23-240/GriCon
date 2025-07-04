using UnityEngine;

public class GameClear : MonoBehaviour
{
    private WarningPresenter warningPresenter;

    private void Awake()
    {
        warningPresenter = new WarningPresenter();
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "PlayerCollision")
        {
            Debug.Log("Game Clear!"); // ゲームクリアのメッセージをログに表示
            // 警告レベル1が解除された場合、レベルを0に戻す
            warningPresenter.WarningModel.WarningLevel.Value = 5;
            UnityEngine.SceneManagement.SceneManager.LoadScene("GameClear");
        }
    }
}