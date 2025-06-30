using UnityEngine;
using UnityEngine.EventSystems;

/// <summary>
/// 「再スタート」ボタンの処理を担当するスクリプト
/// ゲームの状態を初期化し、プレイヤーを再稼働させる
/// </summary>
public class ReStartButton : MonoBehaviour
{
    [SerializeField] private GameObject player; // CircleHandle をアサインする予定（再取得で上書きされる）

    /// <summary>
    /// ゲーム開始時に CircleHandle を自動取得し、player として登録
    /// </summary>
    void Start()
    {
        // シーン上の "CircleHandle" オブジェクトを検索して player に設定
        GameObject circleHandle = GameObject.Find("CircleHandle");
        if (circleHandle != null)
        {
            player = circleHandle;
            Debug.Log("[診断] player を CircleHandle に上書き");
        }
        else
        {
            Debug.LogError("[ReStartButton] CircleHandle がシーン上に見つかりません！");
        }
    }

    /// <summary>
    /// ボタンクリック時のコールバック（Unityイベントから呼ばれる）
    /// </summary>
    public void OnClick()
    {
        // UI選択状態にする（WarningUIWatcher用）
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

        ReStart();
    }

    /// <summary>
    /// ゲーム状態をリセットしてプレイヤーの動作を再開させる
    /// </summary>
    private void ReStart()
    {
        // 移動・回転フラグを有効化、タイマーとリセットフラグも初期化
        GameSystem.Instance.SetCanRotate(true);
        GameSystem.Instance.SetCanMove(true);
        GameSystem.isReset = true;
        GameSystem.clearTime = 0;

        // 親Canvas上のダイアログUIを閉じる（ボタンの親の親）
        Destroy(transform.parent.parent.gameObject);

        if (player == null)
        {
            Debug.LogError("[ReStartButton] player が null のままです");
            return;
        }

        // 回転（HandleRotate）を再び有効にする
        var rotate = player.transform.root.GetComponent<HandleRotate>();
        if (rotate != null)
        {
            //rotate.canRotate = true;
        }
        else
        {
            Debug.LogWarning("HandleRotate が Player に見つかりません");
        }
    }
}
