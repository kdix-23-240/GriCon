using UnityEngine;

public class GameClearScene : MonoBehaviour
{
    private SimpleWarningSender simpleWarningSender;

    void Awake()
    {
        simpleWarningSender = GetComponent<SimpleWarningSender>();
        if (simpleWarningSender == null)
        {
            Debug.LogError("SimpleWarningSender component is missing on GameClearScene.");
        }
    }

    void Start()
    {
        simpleWarningSender.WarningResetForce();
    }
}