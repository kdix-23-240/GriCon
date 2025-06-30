using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 障害物やゴールとの衝突時に回転や移動を停止し、
/// モーダル表示や警告信号の送信などを行うスクリプト
/// </summary>
public class HandleCollision : MonoBehaviour
{
    [SerializeField] private Canvas parent = default;             // モーダルを表示する親Canvas
    [SerializeField] private GameObject biribiriModal = default;  // Stick（障害物）用モーダル
    [SerializeField] private GameObject goalModal = default;      // Goal用モーダル

    // 衝突検出時に呼び出される処理
    void OnCollisionEnter(Collision collision)
    {
        // 衝突相手が「Stick」または「Goal」の場合のみ処理
        if (collision.gameObject.CompareTag("Stick") || collision.gameObject.CompareTag("Goal"))
        {
            // プレイヤーの回転と移動を禁止
            GameSystem.Instance.SetCanRotate(false);
            GameSystem.Instance.SetCanMove(false);

            // 回転スクリプトを無効化
            var playerObj = transform.root;
            var rotate = playerObj.GetComponent<HandleRotate>();
            if (rotate != null)
            {
                //rotate.canRotate = false;
                Debug.Log("[HandleCollision] 回転停止（Player経由）");
            }

            // 該当のモーダル（biribiriModal または goalModal）をCanvas下に表示
            GameObject dialogPrefab = collision.gameObject.CompareTag("Stick") ? biribiriModal : goalModal;
            var dialog = Instantiate(dialogPrefab);
            dialog.transform.SetParent(parent.transform, false);

            // Stick（障害物）に衝突した場合の追加処理
            if (collision.gameObject.CompareTag("Stick"))
            {
                Debug.Log("Calling StartWarningSequence()");
                var warningManager = FindObjectOfType<WarningManager>();
                //warningManager?.StartWarningSequence(); // ← 送信管理はWarningManagerに任せる
                Debug.Log("Game Over");
            }
        }
    }
}
