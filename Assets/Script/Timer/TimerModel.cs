using System.Collections;
using System.Threading.Tasks;
using UniRx;
using UnityEngine;

/// <summary>
/// どのクラスからも使える汎用タイマー
/// コルーチン実行はMonoBehaviour側で行う
/// </summary>
public class TimerModel
{
    public ReactiveProperty<float> time { get; } = new ReactiveProperty<float>(0f);
    public ReactiveProperty<bool> IsCompleted { get; } = new ReactiveProperty<bool>(false);
    private bool isRunning = false;

    /// <summary>
    /// タイマーコルーチン
    /// 非同期処理の途中でisRunningがfalseになった場合は終了
    /// </summary>
    /// <param name="duration">タイマー時間（秒）</param>
    /// <returns></returns>
    public IEnumerator TimerCoroutine(float duration)
    {
        isRunning = true;
        float startTime = Time.time;
        while (Time.time - startTime < duration)
        {
            if (!isRunning) yield break; // isRunningがfalseなら終了
            time.Value = Time.time - startTime;
            yield return null;
        }

        //Debug.Log("Timer completed.");
        IsCompleted.Value = true; // タイマー完了を通知
    }

    /// <summary>
    /// タイマーをリセット
    /// </summary>
    public void Reset()
    {
        isRunning = false;
        IsCompleted.Value = false;
        time.Value = 0f;
    }
}