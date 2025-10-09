using UnityEngine;

public class GripSkelton : MonoBehaviour, IGripAction
{
    [SerializeField] private GameObject[] parts;
    [SerializeField] private float maxBend = 15f; // Maximum bend value for the grip

    /// <summary>
    /// bend�̊������擾
    /// ���̊����ɉ����Ċe�p�[�c�𓧖�������
    /// </summary>
    /// <param name="bend"></param>

    public void OnGrip(float bend)
    {
        float bendRatio = 0;
        if (bend < 3f)
        {
            bendRatio = 0f;
        }
        else if (3f < bend && bend < 7f)
        {
            bendRatio = (bend - 3f) / (7f - 3f);
        }
        else if (bend >= 7f)
        {
            bendRatio = 1f;
        }
        Debug.Log($"GripSkelton OnGrip bend: {bend}, bendRatio: {bendRatio}");

        for (int i = 0; i < parts.Length; i++)
        {
            float alpha = 1f - bendRatio; // ���ׂẴp�[�c�����������œ�����
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