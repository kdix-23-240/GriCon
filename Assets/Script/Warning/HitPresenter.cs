using UnityEngine;
using UniRx;

public class HitPresenter : MonoBehaviour
{
    [SerializeField] private GameObject warningParent;
    private HandleOutSideWarning outSideWarning;
    private HandleMiddleWarning middleWarning;
    private HandleInSideWarning inSideWarning;
    private PlayerCollision playerCollision;
    [SerializeField] private GameClearObj gameClear;
    private WarningManager warningManager;

    private void Awake()
    {
        outSideWarning = warningParent.transform.Find("LowLevelWarning").gameObject.GetComponent<HandleOutSideWarning>();
        middleWarning = warningParent.transform.Find("MiddleLevelWarning").gameObject.GetComponent<HandleMiddleWarning>();
        inSideWarning = warningParent.transform.Find("HighLevelWarning").gameObject.GetComponent<HandleInSideWarning>();
        playerCollision = warningParent.transform.Find("PlayerCollision").gameObject.GetComponent<PlayerCollision>();
        warningManager = GetComponent<WarningManager>();
    }
    private void Start()
    {
        if (outSideWarning == null)
        {
            Debug.LogError("äOë§ÇÃåxçêÇ™ÇπÇ¡ÇƒÇ¢Ç≥ÇÍÇƒÇ¢Ç‹ÇπÇÒ");
        }
        if (middleWarning == null)
        {
            Debug.LogError("íÜä‘ÇÃåxçêÇ™ÇπÇ¡ÇƒÇ¢Ç≥ÇÍÇƒÇ¢Ç‹ÇπÇÒ");
        }
        if (inSideWarning == null)
        {
            Debug.LogError("ì‡ë§ÇÃåxçêÇ™ÇπÇ¡ÇƒÇ¢Ç≥ÇÍÇƒÇ¢Ç‹ÇπÇÒ");
        }
        if (playerCollision == null)
        {
            Debug.LogError("ívñΩìIÇ»è’ìÀÇÃåxçêÇ™ÇπÇ¡ÇƒÇ¢Ç≥ÇÍÇƒÇ¢Ç‹ÇπÇÒ");
        }
        if (warningManager == null)
        {
            Debug.LogError("WarningManagerÇ™å©Ç¬Ç©ÇËÇ‹ÇπÇÒÅBHitPresenterÇ…ÉAÉ^ÉbÉ`ÇµÇƒÇ≠ÇæÇ≥Ç¢ÅB");
        }

        Bind();
    }

    private void Bind()
    {
        Debug.Log("Bind");
        outSideWarning.IsHit
            .Subscribe(isHit => 
            {
                Debug.Log($"äOë§ÇÃåxçê: {isHit}");
                warningManager.ObserveWarningLevel1(isHit);
            })
            .AddTo(this);

        middleWarning.IsHit
            .Subscribe(isHit => 
            {
                Debug.Log($"íÜä‘ÇÃåxçê: {isHit}");
                warningManager.ObserveWarningLevel2(isHit);
            })
            .AddTo(this);

        inSideWarning.IsHit
            .Subscribe(isHit => 
            {
                Debug.Log($"ì‡ë§ÇÃåxçê: {isHit}");
                warningManager.ObserveWarningLevel3(isHit);
            })
            .AddTo(this);

        playerCollision.IsHit
            .Subscribe(isHit => 
            {
                Debug.Log($"ívñΩìIÇ»è’ìÀ: {isHit}");
                warningManager.ObserveWarningLevel4(isHit);
            })
            .AddTo(this);
        gameClear.IsHit
            .Subscribe(isHit => 
            {
                Debug.Log($"ÉQÅ[ÉÄÉNÉäÉA: {isHit}");
                warningManager.ObserveGameClear(isHit);
            })
            .AddTo(this);
    }
}