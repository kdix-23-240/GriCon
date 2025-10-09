using UnityEngine;

public class GripMove : MonoBehaviour, IGripAction
{
    [SerializeField] private GameObject circleHandle;         // �i�s�����̊�ƂȂ�n���h���I�u�W�F�N�g
    [SerializeField] private float firstPlayerPositionX;      // �����ʒuX
    [SerializeField] private float firstPlayerPositionY;      // �����ʒuY
    [SerializeField] private float firstPlayerPositionZ;      // �����ʒuZ
    [SerializeField] private float speedRate = 1;             // ���x���߂̂��߂̒萔
    [SerializeField] private float firstRate = 1;             // ���x���߂̂��߂̒萔

    public void OnGrip(float bend)
    {
        if (!GameSystem.canMove) return; // �ړ��\���`�F�b�N
        Move(bend);
    }

    public void ExitGrip()
    {
        //Debug.Log("Grip exited");
    }

    /// <summary>
    /// �n���h���̌����i�������j�ɑ΂��ăv���C���[���ړ�������
    /// </summary>
    private void Move(float moveSpeed)
    {
        Vector3 moveDirection = -circleHandle.transform.up; // �n���h���̉��������ړ������ɐݒ�
        transform.Translate((moveDirection.normalized * moveSpeed * moveSpeed * speedRate) + (moveDirection.normalized * firstRate), Space.World);
    }

    /// <summary>
    /// �v���C���[�̈ʒu�Ɗp�x��������ԂɃ��Z�b�g����
    /// </summary>
    public void ResetPlayerPosition()
    {
        transform.position = new Vector3(firstPlayerPositionX, firstPlayerPositionY, firstPlayerPositionZ); // �����ʒu�ɖ߂�
        transform.rotation = Quaternion.Euler(0, 0, 0); // ��]��������
    }
}