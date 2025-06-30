using System.Drawing.Text;
using UnityEngine;

public class GriConDirectionSetting : MonoBehaviour
{
    private bool isSet1 = false; // 1方向目の設定が完了したかどうか
    private bool isSet2 = false; // 2方向目の設定が完了したかどうか
    private bool isSet3 = false; // 3方向目の設定が完了したかどうか
    private bool isSet4 = false; // 4方向目の設定が完了したかどうか
    private float flameCounter = 0f; // フレームカウンター
    [SerializeField] private float selectTime = 2f; // ステージ選択のための時間閾値
    public static string stageName = null; // ステージ名

    private void Update()
    {
        CheckIsSet(); // 各方向の設定を確認

        if (CheckIsAllSet())
        {
            flameCounter += Time.deltaTime; // フレームカウンターを更新
        }
        else
        {
            flameCounter = 0f; // 全方向の設定が完了していない場合はカウンターをリセット
        }

        if(flameCounter >= selectTime)
        {
            Debug.Log("全方向の設定が完了しました。シーンを変更します。");
            UnityEngine.SceneManagement.SceneManager.LoadScene(stageName);
            stageName = null; // シーン変更後はステージ名をリセット
        }
    }

    /// <summary>
    /// このオブジェクトの子コンポーネントのカメラを取得し、そのカメラから６方向にrayを飛ばす
    /// 各方向に当たったオブジェクトの名前を取得し、その番号の方向の設定を完了する
    /// rayが外れたら設定を完了しない
    /// </summary>
    private void CheckIsSet()
    {
        Camera camera = GetComponentInChildren<Camera>();
        if (camera == null)
        {
            Debug.LogError("GriConDirectionSetting: 子オブジェクトにカメラが見つかりません。");
            return;
        }
        RaycastHit hit;
        Vector3[] directions = new Vector3[]
        {
            camera.transform.forward, // 前方
            -camera.transform.forward, // 後方
            camera.transform.right, // 右
            -camera.transform.right, // 左
            camera.transform.up, // 上
            -camera.transform.up // 下
        };
        for (int i = 0; i < directions.Length; i++)
        {
            if (Physics.Raycast(camera.transform.position, directions[i], out hit))
            {
                string objectName = hit.collider.gameObject.name;
                Debug.Log($"方向 {i + 1} のオブジェクト名: {objectName}");
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
                Debug.Log($"方向 {i + 1} のオブジェクトが見つかりません。設定は完了しません。");
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
        if(isSet1 && isSet2 && isSet3 && isSet4)
        {
            Debug.Log("全方向の設定が完了しました。");
            return true;
        }
        else
        {
            Debug.Log("全方向の設定が完了していません。");
            return false;
        }
    }
}