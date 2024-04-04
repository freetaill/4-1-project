using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;

public interface character
{
    int ReadStepCount();//������ ���� �� ��� ����
    int CalcStepChange();//���� �� ��ȭ�� ���
    void Move();//ĳ���� ������

    void Exp();//�������� ����ġ ���

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
