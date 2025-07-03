using UnityEngine;

public class GripAnimation : MonoBehaviour
{
    void Start()
    {
        Hide(); // 初期状態では非表示
    }

    public void Show()
    {
        gameObject.SetActive(true); // オブジェクトを表示
    }

    public void Hide()
    {
        gameObject.SetActive(false); // オブジェクトを非表示
    }
}