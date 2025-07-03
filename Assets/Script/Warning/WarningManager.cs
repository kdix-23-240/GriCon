using UnityEngine;

/// <summary>
/// プレイヤーの位置に基づいて警告レベルを判断し、
/// また、衝突時の段階的警告（4→5）とUI操作時の即時警告（5）を送信する統合マネージャ
/// </summary>
public class WarningManager : MonoBehaviour
{
    private WarningPresenter warningPresenter; // 警告を表示するプレゼンター

    private void Awake()
    {
        warningPresenter = GetComponent<WarningPresenter>();
        if (warningPresenter == null)
        {
            Debug.LogError("WarningPresenterが見つかりません。WarningManagerにアタッチしてください。");
        }
    }

    /// <summary>
    /// ゲーム中の警告領域に応じて警告レベルを監視（自動送信はしない）
    /// </summary>
    public void ObserveWarningLevel1(bool isHit)
    {
        if (isHit)
        {
            warningPresenter.WarningModel.WarningLevel.Value = 3;
        }
        else
        {
            // 警告レベル1が解除された場合、レベルを0に戻す
            warningPresenter.WarningModel.WarningLevel.Value = 5;
        }
    }

    public void ObserveWarningLevel2(bool isHit)
    {
        if (isHit)
        {
            warningPresenter.WarningModel.WarningLevel.Value = 2;
        }
        else
        {
            // 警告レベル2が解除された場合、レベルを1に戻す
            warningPresenter.WarningModel.WarningLevel.Value = 3;
        }
    }
    public void ObserveWarningLevel3(bool isHit)
    {
        if (isHit)
        {
            warningPresenter.WarningModel.WarningLevel.Value = 1;
        }
        else
        {
            // 警告レベル3が解除された場合、レベルを2に戻す
            warningPresenter.WarningModel.WarningLevel.Value = 2;
        }
    }

    public void ObserveWarningLevel4(bool isHit)
    {
        if (isHit)
        {
            warningPresenter.WarningModel.WarningLevel.Value = 4;
        }
        else
        {
            // 警告レベル4が解除された場合、レベルを3に戻す
            warningPresenter.WarningModel.WarningLevel.Value = 1;
        }
    }
}