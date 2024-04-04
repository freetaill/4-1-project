using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;

public interface character
{
    int ReadStepCount();//센서로 걸음 수 들고 오기
    int CalcStepChange();//걸음 수 변화량 계산
    void Move();//캐릭터 움직임

    void Exp();//걸음수로 경험치 계산

}

public class characterMove : MonoBehaviour
{
    Animator anim;
    private Rigidbody myRigid;
    private float walkSpeed = 10f;
    Vector3 moveVec;
    float hAxis;
    float vAxis;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        myRigid = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        hAxis = Input.GetAxisRaw("Horizontal");
        vAxis = Input.GetAxisRaw("Vertical");

        moveVec = new Vector3(hAxis,0,vAxis).normalized;

        transform.position += moveVec * walkSpeed * Time.deltaTime;
        anim.SetBool("isMove", moveVec != Vector3.zero);

        transform.LookAt(transform.position+moveVec);
    }
}
