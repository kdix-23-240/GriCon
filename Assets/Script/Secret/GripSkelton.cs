using UnityEngine;

public class GripSkelton : MonoBehaviour, IGripAction
{
    [SerializeField] private GameObject[] parts;
    [SerializeField] private float maxBend = 15f; // Maximum bend value for the grip

    /// <summary>
    /// bendの割合を取得
    /// その割合に応じて各パーツを透明化する
    /// </summary>
    /// <param name="bend"></param>

    public void OnGrip(float bend)
    {
        float bendRatio = 0;
        if (bend < 4f)
        {
            bendRatio = 0f;
        }
        else if (4f < bend && bend < 10f)
        {
            bendRatio = (bend - 4f) / (10f - 4f);
        }
        else if (bend >= 10f)
        {
            bendRatio = 1f;
        }
        Debug.Log($"GripSkelton OnGrip bend: {bend}, bendRatio: {bendRatio}");

        for (int i = 0; i < parts.Length; i++)
        {
            float alpha = 1f - bendRatio; // すべてのパーツが同じ割合で透明化
            MeshRenderer meshRenderer = parts[i].GetComponent<MeshRenderer>();
            if (meshRenderer != null)
            {
                Color color = meshRenderer.material.color;
                color.a = alpha;
                meshRenderer.material.color = color;
            }
        }
    }

    public void ExitGrip()
    {
        for (int i = 0; i < parts.Length; i++)
        {
            MeshRenderer meshRenderer = parts[i].GetComponent<MeshRenderer>();
            if (meshRenderer != null)
            {
                Color color = meshRenderer.material.color;
                color.a = 1f;
                meshRenderer.material.color = color;
            }
        }
    }
}