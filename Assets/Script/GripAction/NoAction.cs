using UnityEngine;

/// <summary>
/// コントローラーの角度調整シーンで使用される、握っても何もしないグリップアクションのクラス
/// </summary>
public class NoAction : MonoBehaviour, IGripAction
{
    public void OnGrip(float bend)
    {
        // Grip action does nothing
    }

    public void ExitGrip()
    {
        // Exit grip action does nothing
    }
}