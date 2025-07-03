using UnityEngine;

public class SelectBaseAnimationView : MonoBehaviour
{
    [SerializeField] private GameObject firstStageSelectedAnimation;
    [SerializeField] private GameObject secondStageSelectedAnimation;
    [SerializeField] private GameObject thirdStageSelectedAnimation;

    private void Start()
    {
        firstStageSelectedAnimation.SetActive(false);
        secondStageSelectedAnimation.SetActive(false);
        thirdStageSelectedAnimation.SetActive(false);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("OnTriggerEnter2D: " + other.name);
        if (other.name == "1")
        {
            firstStageSelectedAnimation.SetActive(true);
        }
        else if (other.name == "2")
        {
            secondStageSelectedAnimation.SetActive(true);
        }
        else if (other.name == "3")
        {
            thirdStageSelectedAnimation.SetActive(true);
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        Debug.Log("OnTriggerExit2D: " + other.name);
        if (other.name == "1")
        {
            firstStageSelectedAnimation.SetActive(false);
        }
        else if (other.name == "2")
        {
            secondStageSelectedAnimation.SetActive(false);
        }
        else if (other.name == "3")
        {
            thirdStageSelectedAnimation.SetActive(false);
        }
    }
}