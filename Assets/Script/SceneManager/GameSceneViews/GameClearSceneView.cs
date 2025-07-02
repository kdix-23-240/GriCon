using UnityEngine;

public class GameClearSceneView : MonoBehaviour, IGameSceneView
{
    public void Initialize()
    {
        Hide(); // ‰Šú‰»‚É”ñ•\¦‚É‚·‚é
    }

    public void Show()
    {
        gameObject.SetActive(true);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }
}