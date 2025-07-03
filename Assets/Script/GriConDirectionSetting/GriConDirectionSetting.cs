using UnityEngine;

/// <summary>
/// コントローラーの角度調整をするクラス
/// </summary>

public class GriConDirectionSetting : MonoBehaviour
{
    private bool isSet1 = false;
    private bool isSet2 = false;
    private bool isSet3 = false;
    private bool isSet4 = false;
    [SerializeField] private TimerPresenter timer;
    [SerializeField] private float selectTime = 2f;
    [SerializeField] private GameObject gripAnimationObject; // グリップアニメーションのオブジェクト

    // 各方向のGriConDirectionBaseを格納
    [SerializeField] private GriConDirectionBase[] directionObjects = new GriConDirectionBase[4];

    void Start()
    {
        if (timer == null)
        {
            Debug.LogError("GriConDirectionSetting: タイマーオブジェクトが設定されていません。");
            return;
        }
    }

    private void Update()
    {
        CheckIsSet(); // 常に基準に合うかどうかを確認する

        if (CheckIsAllSet())
        {
            timer.StartTimer(selectTime);
            gripAnimationObject.GetComponent<GripAnimation>().Show(); // グリップアニメーションを表示
        }
        else
        {
            timer.ResetTimer();
            gripAnimationObject.GetComponent<GripAnimation>().Hide(); // グリップアニメーションを非表示
        }
    }

    /// <summary>
    /// 基準オブジェクトを使って角度があっているか判断するクラス
    /// </summary>
    private void CheckIsSet()
    {
        // カメラから
        Camera camera = GetComponentInChildren<Camera>();
        if (camera == null)
        {
            //Debug.LogError("GriConDirectionSetting: 子オブジェクトにカメラが見つかりません。");
            return;
        }
        RaycastHit hit;
        Vector3[] directions = new Vector3[]
        {
            camera.transform.forward,
            -camera.transform.forward,
            camera.transform.right,
            -camera.transform.right
        };

        // 各方向にRayを飛ばして角度が合えばそれに対応する真偽値を設定する
        for (int i = 0; i < directions.Length; i++)
        {
            bool isHit = Physics.Raycast(camera.transform.position, directions[i], out hit);
            if (isHit)
            {
                var dirBase = hit.collider.gameObject.GetComponent<GriConDirectionBase>();
                if (dirBase != null)
                {
                    dirBase.OnChecked();
                }
                switch (i)
                {
                    case 0: isSet1 = true; break;
                    case 1: isSet2 = true; break;
                    case 2: isSet3 = true; break;
                    case 3: isSet4 = true; break;
                }
            }
            else
            {
                // 基準から離れると基準オブジェクトの色を変える
                if (directionObjects[i] != null)
                {
                    directionObjects[i].OnUnchecked();
                }
                //Debug.Log($"方向 {i + 1} のオブジェクトが見つかりません。設定は完了しません。");
                switch (i)
                {
                    case 0: isSet1 = false; break;
                    case 1: isSet2 = false; break;
                    case 2: isSet3 = false; break;
                    case 3: isSet4 = false; break;
                }
            }
        }
    }

    private bool CheckIsAllSet()
    {
        if (isSet1 && isSet2 && isSet3 && isSet4)
        {
            //Debug.Log("全方向の設定が完了しました。");
            return true;
        }
        else
        {
            //Debug.Log("全方向の設定が完了していません。");
            return false;
        }
    }
}