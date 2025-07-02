using Unity.VisualScripting;
using UnityEngine;

public class GameOverSceneView : MonoBehaviour, IGameSceneView
{
    public void Initialize()
    {
        Hide();
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