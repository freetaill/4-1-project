using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class btn_Exercise : MonoBehaviour
{
    [Header("Text")]
    public Text stepsText;
    public Text GoldText;

    [Header("character")]
    public Rigidbody player;

    [Header("Animator")]
    public Animator act_play;

    [Header("Move")]
    public float moveSpeed;

    float Waitingtime;
    float timer;
    int FirstStep;
    int post_count = 0;
    int count;
    int Gold;

    // Start is called before the first frame update
    void Start()
    {
        FirstStep = StepCounter.current.stepCounter.ReadValue();
        count = 0;
        timer = 1.0f;
        Waitingtime = 1.0f;
    }
    private void Update()
    {
        if (!LinearAccelerationSensor.current.enabled)
        {
            Debug.Log("The Device is not enabled");
            InputSystem.EnableDevice(LinearAccelerationSensor.current);
        }

        if (!StepCounter.current.enabled)
        {
            //initText.text = "Stepcounter is Paused";
            InputSystem.EnableDevice(StepCounter.current);
        }
        else
        {
            if (FirstStep != 0)
            {
                count = StepCounter.current.stepCounter.ReadValue() - FirstStep;
                stepsText.text = count.ToString();
                Gold = GameManager.instance.player.Get_coin();
                GoldText.text = Gold.ToString();
                Move();
                post_count = count;
                //float gyro_x = UnityEngine.InputSystem.Gyroscope.current.angularVelocity.x.ReadValue();
                //gyroText_x.text = gyro_x.ToString();
                //gyroText_y.text = UnityEngine.InputSystem.Gyroscope.current.angularVelocity.y.ReadValue().ToString();
                //gyroText_z.text = UnityEngine.InputSystem.Gyroscope.current.angularVelocity.z.ReadValue().ToString();
                //animator_fight(gyro_x);
            }
            else
            {
                getCount();
            }
        }
    }

    void getCount()
    {
        FirstStep = StepCounter.current.stepCounter.ReadValue();
        //initText.text = FirstStep.ToString();
    }

    void Move()
    {
        if (count > 0 && post_count != count)
        {
            if (!act_play.GetBool("Run"))
            {
                act_play.SetBool("Run", true);
            }
            // �̵� ���͸� ����ȭ�Ͽ� �̵� �ӵ��� �ð� ������ ���� �� ���� ��ġ�� ����
            player.transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);
            //player.AddForce(Vector3.forward * moveSpeed * Time.deltaTime, ForceMode.VelocityChange);
        }
    }

    //��ư �̺�Ʈ
    public void Go_Main()
    {
        SceneManager.LoadScene("Main_Scene");
    }
}
