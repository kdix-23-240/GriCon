using UnityEngine;
using UniRx;

public class ControllerDataPresenter : MonoBehaviour
{
    private ControllerDataViewRotate rotateView;
    [SerializeField] private float bendWall = 4f;
    private IGripAction gripAction; // GripAction interface for handling grip actions

    void Awake()
    {
        rotateView = GetComponent<ControllerDataViewRotate>();
        gripAction = GetComponent<IGripAction>();
    }
    void Start()
    {
        if(gripAction == null)
        {
            Debug.LogError("ControllerDataPresenter:GripActionがアタッチされていません");
        }
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
            if(bendWall < bend)
            {
                gripAction.OnGrip(bend);
            }
            else
            {
                gripAction.ExitGrip();
            }
        })
        .AddTo(this);
    }
}