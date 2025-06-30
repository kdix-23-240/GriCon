using System.Collections;
using UniRx;
using UnityEngine;

/// <summary>
/// どのクラスからも使える汎用タイマー
/// コルーチン実行はMonoBehaviour側で行う
/// </summary>
public class TimerModel
{
    public ReactiveProperty<bool> IsCompleted { get; } = new ReactiveProperty<bool>(false);
    public float ElapsedTime { get; private set; } = 0f;

    /// <summary>
    /// タイマーコルーチン
    /// </summary>
    /// <param name="duration">タイマー時間（秒）</param>
    /// <returns></returns>
    public IEnumerator TimerCoroutine(float duration)
    {
        IsCompleted.Value = false;
        ElapsedTime = 0f;
        float startTime = Time.time;
        while (Time.time - startTime < duration)
        {
            ElapsedTime = Time.time - startTime;
            yield return null;
        }
        ElapsedTime = duration;
        IsCompleted.Value = true;
        Debug.Log($"[UniversalTimer] Timer ended after {ElapsedTime} seconds.");
    }

    /// <summary>
    /// タイマーをリセット
    /// </summary>
    public void Reset()
    {
        IsCompleted.Value = false;
        ElapsedTime = 0f;
    }
}