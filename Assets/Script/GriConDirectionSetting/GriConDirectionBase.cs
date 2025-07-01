using UnityEngine;

public class GriConDirectionBase : MonoBehaviour
{
    [SerializeField] private Material defaultMaterial; // デフォルトのマテリアル
    [SerializeField] private Material checkMaterial; // チェック済みのマテリアル

    public void OnChecked()
    {
        gameObject.GetComponent<Renderer>().material = checkMaterial; // チェック済みのマテリアルに変更
    }

    public void OnUnchecked()
    {
        gameObject.GetComponent<Renderer>().material = defaultMaterial; // デフォルトのマテリアルに戻す
    }
}