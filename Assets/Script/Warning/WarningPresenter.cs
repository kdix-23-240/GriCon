using UnityEngine;
using UniRx;
using System.Runtime.InteropServices;

public class WarningPresenter : MonoBehaviour
{
    private WarningModel warningModel;
    public WarningModel WarningModel { get { return warningModel; } }
    private WarningSender warningSender;
    private void Awake()
    {
        warningModel = new WarningModel();
        warningSender = GetComponent<WarningSender>();
    }
    private void Start()
    {
        Bind();
    }
    private void Bind()
    {
        warningModel.WarningLevel.Skip(1).Subscribe(level =>
        {
            Debug.Log("WarningPresenter:WarningLevelÇ™ïœçXÇ≥ÇÍÇ‹ÇµÇΩ");
            Debug.Log($"WarningPresenter:WarningLevel = {level}");
            warningSender.OnChangeWarningLevel(level);
        }).AddTo(this);
    }
}