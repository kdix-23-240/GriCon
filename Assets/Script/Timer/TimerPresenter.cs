using UnityEngine;
using UniRx;
using System.Xml.Serialization;

public class TimerPresenter : MonoBehaviour
{
    private TimerModel model; // タイマーモデルのインスタンス
    private TimerView view; // タイマー表示用のビュー

    private void Awake()
    {
        // タイマーモデルのインスタンスを生成
        model = new TimerModel();
        view = GetComponent<TimerView>();
    }

    void Start()
    {
        Bind();
    }

    public void StartTimer(float duration)
    {
        // タイマーコルーチンを開始
        StartCoroutine(model.TimerCoroutine(duration));
        //Debug.Log($"Timer started for {duration} seconds.");
    }

    public void ResetTimer()
    {
        // タイマーをリセット
        model.Reset();
        //Debug.Log("Timer has been reset.");
    }
    private void Bind()
    {

        model.time.Subscribe(time =>
        {
            // タイマーの時間をビューに反映
            view.UpdateTime(time);
        })
           .AddTo(this); // このGameObjectが破棄されると自動的に購読解除されるようにする
        model.IsCompleted
            .Subscribe(isCompleted =>
            {
                if (isCompleted)
                {
                    // タイマー完了時の処理をここに追加

                }
            })
            .AddTo(this); // このGameObjectが破棄されると自動的に購読解除されるようにする
    }

    public TimerModel GetModel()
    {
        return model; // モデルを返す
    }
}