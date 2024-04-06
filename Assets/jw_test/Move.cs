using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{
    [Header("Move")]
    public float moveSpeed;

    void Move_p()
    {
        // 입력에 따라 이동 방향 벡터 계산
        Vector3 moveVec = transform.forward * 1;

        // 이동 벡터를 정규화하여 이동 속도와 시간 간격을 곱한 후 현재 위치에 더함
        transform.position += moveVec.normalized * moveSpeed * Time.deltaTime;
    }
}
