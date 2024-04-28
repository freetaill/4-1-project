using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.Android;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Android;

using UnityEngine.UI;

public class test : MonoBehaviour
{
    //[Header("Steps")]
    public int count = 0;
    [Header("Text")]
    public Text stepsText;
    [SerializeField]
    public Text accelText;
    [SerializeField]
    public Text gyroText_x;
    public Text gyroText_y;
    public Text gyroText_z;
    [SerializeField]
    public AndroidStepCounter asc;
    [SerializeField]
    public Text DebugText;
    int FirstStep;
    int post_count = 0;

    [Header("character")]
    public Rigidbody player;

    [Header("Move")]
    public float moveSpeed;

    public Animator act_play;
    public Text Attack;
    public Text Defend;
    public int Waitingtime;
    float timer;

    public event EventHandler ValueChanged;

    int k;

    // Start is called before the first frame update
    /*void OnEnable()
    {
        if (!Permission.HasUserAuthorizedPermission("android.permission.ACTIVITY_RECOGNITION"))
        {
            Permission.RequestUserPermission("android.permission.ACTIVITY_RECOGNITION");
        }
    }*/

    void getCount(){
        FirstStep = StepCounter.current.stepCounter.ReadValue();
        //initText.text = FirstStep.ToString();
    }
 
    private void Start()
    {
        if (!Permission.HasUserAuthorizedPermission("android.permission.ACTIVITY_RECOGNITION"))
        {
            Permission.RequestUserPermission("android.permission.ACTIVITY_RECOGNITION");
        }
        if (!LinearAccelerationSensor.current.enabled)
        {
            InputSystem.EnableDevice(LinearAccelerationSensor.current);
        }

        if (!StepCounter.current.enabled)
        {
            InputSystem.EnableDevice(StepCounter.current);
        }
        
        if(!UnityEngine.InputSystem.Gyroscope.current.enabled)
        {
            InputSystem.EnableDevice(UnityEngine.InputSystem.Gyroscope.current);
        }
        count = 0;
        
        FirstStep = StepCounter.current.stepCounter.ReadValue();

        act_play.SetBool("Stand", false);

        timer= 0;
        Waitingtime = 1;
    }
 
 
    // Update is called once per frame
    void Update()
    {
        if (!LinearAccelerationSensor.current.enabled)
        {
            accelText.text = "The Device is not enabled";
            InputSystem.EnableDevice(LinearAccelerationSensor.current);
        }
        else
        {
            accelText.text = LinearAccelerationSensor.current.acceleration.ReadValue().ToString();
        }
 
        if (!StepCounter.current.enabled)
        {
            //initText.text = "Stepcounter is Paused";
            InputSystem.EnableDevice(StepCounter.current);
        }
        else
        {
            if(FirstStep != 0){
                count = StepCounter.current.stepCounter.ReadValue()-FirstStep;
                stepsText.text = "Steps: " + count.ToString();
                Move();
                post_count = count;
                float gyro_x = UnityEngine.InputSystem.Gyroscope.current.angularVelocity.x.ReadValue();
                gyroText_x.text = gyro_x.ToString();
                gyroText_y.text = UnityEngine.InputSystem.Gyroscope.current.angularVelocity.y.ReadValue().ToString();
                gyroText_z.text = UnityEngine.InputSystem.Gyroscope.current.angularVelocity.z.ReadValue().ToString();
                animator(gyro_x);
            }
            else{
                getCount();
            }
        }
    }

    void Move()
    {
        if (count > 0 && post_count != count)
        {
            // �̵� ���͸� ����ȭ�Ͽ� �̵� �ӵ��� �ð� ������ ���� �� ���� ��ġ�� ����
            player.transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);
            //player.AddForce(Vector3.forward * moveSpeed * Time.deltaTime, ForceMode.VelocityChange);
        }
    }

    void animator(float gyro_x)
    {
        timer += Time.deltaTime;

        if (timer > Waitingtime)
        {
            if (gyro_x < -1)
            {
                if(act_play.GetBool("Run") == true){
                    act_play.SetBool("Run", false);
                }
                act_play.SetBool("Attack", true);
                Attack.text = "Attack";
                Defend.text = "";
            }
            else if (gyro_x > 1)
            {
                if(act_play.GetBool("Run") == true){
                    act_play.SetBool("Run", false);
                }
                act_play.SetBool("Stand", false);
                act_play.SetTrigger("Defend");
                Attack.text = "";
                Defend.text = "Defend";
            }
        }
    }
}