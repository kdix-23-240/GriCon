using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseButton : MonoBehaviour
{
    [SerializeField] private Canvas parent = default;         // ダイアログを載せるキャンバス
    [SerializeField] private GameObject biribiriModal = default; // 表示するポーズモーダル

    private bool isPaused = false;

    void Update()
    {
        // キー入力 or 他スクリプトからのフラグで Pause を呼び出す設計も可能
        if (Input.GetKeyDown(KeyCode.P))
        {
            OnClick(); // Pキー押下でポーズ切り替え
        }
    }

    /// <summary>
    /// UIボタンや外部イベントから呼び出されるポーズ発火メソッド
    /// </summary>
    public void OnClick()
    {
        if (!isPaused)
        {
            Pause();
        }
    }

    /// <summary>
    /// ポーズ処理：ゲームシステムを停止し、UI表示
    /// </summary>
    private void Pause()
    {
        //GameSystem.Instance.SetCanRotate(false);
        //GameSystem.Instance.SetCanMove(false);
        var _dialog = Instantiate(biribiriModal);
        _dialog.transform.SetParent(parent.transform, false);

        isPaused = true;
    }
}
