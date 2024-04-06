using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{
    [Header("Move")]
    public float moveSpeed;

    void Move_p()
    {
        // �Է¿� ���� �̵� ���� ���� ���
        Vector3 moveVec = transform.forward * 1;

        // �̵� ���͸� ����ȭ�Ͽ� �̵� �ӵ��� �ð� ������ ���� �� ���� ��ġ�� ����
        transform.position += moveVec.normalized * moveSpeed * Time.deltaTime;
    }
}
