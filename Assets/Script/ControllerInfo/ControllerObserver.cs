using UnityEngine;

public class ControllerObserver : MonoBehaviour
{

    void Awake()
    {
        
    }
    void Start()
    {

    }
    void Update()
    {
        // Get_Informationが未初期化の場合はスキップ
        if (Get_Information.Instance == null) return;

        // シリアルから角度データを取得
        float[] data = Get_Information.Instance.GetReceivedData();
        ControllerModel.GetInstance.RotateX.Value = -data[0];
        ControllerModel.GetInstance.RotateY.Value = data[2];// y軸要素とz軸要素が逆になっているので注意
        ControllerModel.GetInstance.RotateZ.Value = data[1];
        ControllerModel.GetInstance.Bend.Value = data[3];
    }
}