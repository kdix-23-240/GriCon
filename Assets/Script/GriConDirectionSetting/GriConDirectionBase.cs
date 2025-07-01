using UnityEngine;

/// <summary>
/// コントローラーの傾きを調整するシーンで基準となるオブジェクトのスクリプト
/// 基準はコントローラーの傾きが正しくなることで色が変わる
/// </summary>
public class GriConDirectionBase : MonoBehaviour
{
    [SerializeField] private Material defaultMaterial; // デフォルトのマテリアル
    [SerializeField] private Material checkMaterial; // チェック済みのマテリアル

    /// <summary>
    /// コントローラーが正しい角度になれば呼ばれる
    /// GriConDirectionSetting.csから呼び出される
    /// </summary>
    public void OnChecked()
    {
        gameObject.GetComponent<Renderer>().material = checkMaterial; // チェック済みのマテリアルに変更
    }

    /// <summary>
    /// コントローラーが正しい角度でなくなった場合に呼ばれる
    /// GriConDirectionSetting.csから呼び出される
    /// </summary>
    public void OnUnchecked()
    {
        gameObject.GetComponent<Renderer>().material = defaultMaterial; // デフォルトのマテリアルに戻す
    }
}