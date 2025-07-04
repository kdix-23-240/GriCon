using UnityEngine;

public class GameOverScene : MonoBehaviour
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
        simpleWarningSender.MaxWarningForce(); // ƒQ[ƒ€ƒNƒŠƒA‚ÌŒx‚ğÅ‘å‚Éİ’è
    }
}