using UnityEngine;
using UniRx;

public class ControllerDataPresenter : MonoBehaviour
{
    private ControllerDataViewRotate rotateView;
    [SerializeField] private TimerPresenter timerPresenter;

    void Awake()
    {
        rotateView = GetComponent<ControllerDataViewRotate>();
    }
    void Start()
    {
        Bind();
    }

    private void Bind()
    {
        ControllerDataModel.GetInstance.RotateX
        .Subscribe(rotate =>
        {
            rotateView.RotateX(rotate);
        })
        .AddTo(this);

        ControllerDataModel.GetInstance.RotateY
        .Subscribe(rotate =>
        {
            rotateView.RotateY(rotate);
        })
        .AddTo(this);

        ControllerDataModel.GetInstance.RotateZ
        .Subscribe(rotate =>
        {
            rotateView.RotateZ(rotate);
        })
        .AddTo(this);

        ControllerDataModel.GetInstance.Bend
        .Subscribe(bend =>
        {
            Debug.Log($"ControllerPresenter: ˆ¬‚Á‚½‚Æ‚«‚Ìˆ—‚ª‚Ü‚¾Bind‚³‚ê‚Ä‚¢‚È‚¢");
        })
        .AddTo(this);
    }
}