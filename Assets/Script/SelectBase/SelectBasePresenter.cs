using UnityEngine;
using UniRx;

public class SelectBasePresenter : MonoBehaviour
{
    private SelectBaseView view;

    void Awake()
    {
        view = GetComponent<SelectBaseView>();
    }
    void Start()
    {
        Bind();
    }

    /// <summary>
    /// コントローラーが傾いたらそれを描画する
    /// 握られたらGripActionを呼び出す
    /// </summary>
    private void Bind()
    {
        ControllerDataModel.GetInstance.RotateZ
        .Subscribe(rotate =>
        {
            view.RotateZ(rotate);
        })
        .AddTo(this);
    }
}