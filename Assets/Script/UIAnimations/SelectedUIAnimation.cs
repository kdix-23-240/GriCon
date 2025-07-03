using UnityEngine;

public class SelectedUIAnimation : MonoBehaviour
{
    [SerializeField] private GameObject animationObject;

    void Start()
    {
        animationObject.SetActive(false);
    }

    public void Show()
    {
        animationObject.SetActive(true);
    }

    public void Hide()
    {
        animationObject.SetActive(false);
    }
}