using UniRx;
using UnityEngine;

public class GameClear : MonoBehaviour
{
    private readonly ReactiveProperty<bool> isHit = new ReactiveProperty<bool>(false);
    public ReactiveProperty<bool> IsHit
    {
        get { return isHit; }
    }

    private void Awake()
    {

    }
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "PlayerCollision")
        {
            Debug.Log("Game Clear!"); // ゲームクリアのメッセージをログに表示
            isHit.Value = true; // ゲームクリアの状態を更新
            UnityEngine.SceneManagement.SceneManager.LoadScene("GameClear");
        }
    }
}