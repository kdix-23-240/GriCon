using UnityEngine;

/// <summary>
/// シリアル通信スクリプト（Get_Information）をゲーム開始時に生成し、
/// シーンが切り替わっても破棄されないように常駐させる初期化スクリプト
/// </summary>
public class SerialInitializer : MonoBehaviour
{
    void Awake()
    {
        // 既にインスタンスが存在する場合は何もしない
        if (Get_Information.Instance != null) return;

        // 新しいGameObjectを生成してGet_Informationをアタッチ
        GameObject obj = new GameObject("Get_Information (Persistent)");
        DontDestroyOnLoad(obj);  // シーン遷移でも破棄されないように設定
        obj.AddComponent<Get_Information>();
    }
}
