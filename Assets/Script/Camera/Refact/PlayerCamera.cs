using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    [SerializeField] private GameObject player = null;
    [SerializeField] private float offsetX = 0.0f; // プレイヤーからのX軸オフセット
    [SerializeField] private float offsetY = 0.0f; // プレイヤーからのY軸オフセット
    [SerializeField] private float offsetZ = 0.0f; // プレイヤーからのZ軸オフセット
    private Vector3 playerPos = Vector3.zero;
    private Vector3 pos= Vector3.zero;

    void Awake()
    {
        if(player == null)
        {
            Debug.LogError("PlayerCamera:プレイヤーがアタッチされていません");
            return;
        }
        playerPos = player.transform.position;
        pos = new Vector3(playerPos.x + offsetX, playerPos.y + offsetY, playerPos.z + offsetZ);
    }

    private void Update()
    {
        ChasePlayer();
    }

    private void ChasePlayer()
    {
        if (player == null) return;
        playerPos = player.transform.position;
        pos = new Vector3(playerPos.x + offsetX, playerPos.y + offsetY, playerPos.z + offsetZ);
        transform.position = pos;
    }
}