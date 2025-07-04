using UniRx;
using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    private readonly ReactiveProperty<bool> isHit = new ReactiveProperty<bool>(false);
    public ReactiveProperty<bool> IsHit
    {
        get { return isHit; }
    }

    void OnCollisionEnter(Collision other)
    {
        //Debug.Log("Hit");
        if (other.gameObject.CompareTag("Stick"))
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene("GameOver");
        }
    }
}