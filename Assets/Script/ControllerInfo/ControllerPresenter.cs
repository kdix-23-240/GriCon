using UnityEngine;
using UniRx;

public class ControllerPresenter : MonoBehaviour
{
    private HandleRotate handleRotate;
    void Awake()
    {
        handleRotate = GetComponent<HandleRotate>();
    }
    void Start()
    {
        Bind();
    }

    private void Bind()
    {
        ControllerModel.GetInstance.RotateX
        .Subscribe(rotate =>
        {
            handleRotate.RotateX(rotate);
        })
            .AddTo(this);

        ControllerModel.GetInstance.RotateY
    .Subscribe(rotate =>
    {
        handleRotate.RotateY(rotate);
    })
        .AddTo(this);

        ControllerModel.GetInstance.RotateZ
    .Subscribe(rotate =>
    {
        handleRotate.RotateZ(rotate);
    })
        .AddTo(this);

    }
}