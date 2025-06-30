using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Start : MonoBehaviour
{
    public void OnClick()
    {
        // UI選択状態にする
        EventSystem.current.SetSelectedGameObject(this.gameObject);
        // 直接シリアル送信で「5」を送る
        if (Get_Information.Instance != null)
        {
            Get_Information.Instance.SetOutgoingByte((byte)'5');
        }
        else
        {
            Debug.LogError("[ReStartButton] Get_Information.Instance が null です");
        }

        StageSelect();
    }

    private void StageSelect()
    {
        // シーンを再読み込み
        UnityEngine.SceneManagement.SceneManager.LoadScene("StageSelect");
        // "Get_Information (Persistent)"を探して変数に格納
        var obj = GameObject.Find("Get_Information (Persistent)");
        obj.AddComponent<ControllerObserver>(); // ControllerObserverを追加
    }
}
