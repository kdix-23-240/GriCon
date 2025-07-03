using UnityEngine;

public class SelectedUIAnimationController : MonoBehaviour
{
    [SerializeField] private Camera controllerCamera; // ステージ選択カメラ
    [SerializeField] private GameObject stageParent; // アニメーションオブジェクトの配列
    private GameObject[] stages; // ステージの親オブジェクト

    private void Awake()
    {
        stages = new GameObject[stageParent.transform.childCount];
        for (int i = 0; i < stageParent.transform.childCount; i++)
        {
            stages[i] = stageParent.transform.GetChild(i).gameObject;
        }
    }

    void Update()
    {
        ShowSelectedAnimation();
    }

    /// <summary>
    /// カメラからrayを飛ばして、ヒットしたオブジェクトのアニメーションを開始する
    /// rayから外れたオブジェクトはアニメーションを停止する
    /// </summary>
    private void ShowSelectedAnimation()
    {
        RaycastHit hit;
        Ray ray = new Ray(controllerCamera.transform.position, controllerCamera.transform.forward);

        if (Physics.Raycast(ray, out hit, 100f)) // レイキャストでオブジェクトを検出
        {
            Debug.Log("ヒットしたオブジェクト: " + hit.collider.gameObject.name);
            hit.collider.gameObject.GetComponent<SelectedUIAnimation>()?.Show();
            for(int i = 0; i < stages.Length; i++)
            {
                if (stages[i] != hit.collider.gameObject)
                {
                    stages[i].GetComponent<SelectedUIAnimation>()?.Hide();
                }
            }
        }
    }
}