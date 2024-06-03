using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Joy_control : MonoBehaviour
{
    public FixedJoystick joystick;
    public float Speed;

    Rigidbody rigid;
    Animator anim;
    Vector3 moveVec;

    // Start is called before the first frame update
    void Awake()
    {
        rigid= GetComponent<Rigidbody>();
        anim= GetComponent<Animator>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float x = joystick.Horizontal;
        float z = joystick.Vertical;

        moveVec = new Vector3(x,0,z) * Speed *Time.deltaTime;
        rigid.MovePosition(rigid.position + moveVec);

        if(moveVec.sqrMagnitude == 0)
        {
            return;
        }

        Quaternion dirQuat = Quaternion.LookRotation(moveVec);
        Quaternion moveQuat = Quaternion.Slerp(rigid.rotation, dirQuat, 0.3f);
        rigid.MoveRotation(moveQuat);
    }

    private void LateUpdate()
    {
        if(moveVec.sqrMagnitude> 0) 
        {
            anim.SetBool("Attack", false);
            anim.SetBool("Defend", false);
        }
        anim.SetFloat("Run", moveVec.sqrMagnitude);
    }
}
