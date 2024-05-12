using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Unity.VisualScripting;
using UnityEngine.Android;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Android;

public class btn_Battle : MonoBehaviour
{
    //[Header("Steps")]
    public int count = 0;
    [Header("Text")]
    public Text GoldText;
    public Text accelText;
    public Text gyroText_x;
    public AndroidStepCounter asc;
    public Text Attack;
    public Text Defend;
    public Text Debuglog;

    [Header("character")]
    public Rigidbody player;
    public Animator act_play;

    [Header("Move")]
    public float moveSpeed;

    public float Waitingtime;
    float timer;
    float gyro_x;

    int Gold;

    private void Start()
    {
        timer = 0.1f;
        Waitingtime = 0.1f;
    }


    // Update is called once per frame
    void Update()
    {
        if (!UnityEngine.InputSystem.Gyroscope.current.enabled)
        {
            Debuglog.text = "Error";
            InputSystem.EnableDevice(UnityEngine.InputSystem.Gyroscope.current);
        }
        else
        {
            //Gold = GameManager.instance.player.Get_coin();
            //GoldText.text = Gold.ToString();
            gyro_x = UnityEngine.InputSystem.Gyroscope.current.angularVelocity.x.ReadValue();
            gyroText_x.text = gyro_x.ToString();
            animator_fight(gyro_x);
        }
    }

    void animator_fight(float Gyro_x)
    {
        timer += Time.deltaTime;

        if (timer >= Waitingtime)
        {
            if (Gyro_x < -1)
            {
                if (act_play.GetBool("Run") == true)
                {
                    act_play.SetBool("Run", false);
                }
                act_play.SetBool("Attack", true);
                act_play.SetBool("Defend", false);
                Attack.text = "Attack";
                Defend.text = "";
                timer = 0;
            }
            else if (Gyro_x > 1)
            {
                if (act_play.GetBool("Run") == true)
                {
                    act_play.SetBool("Run", false);
                }
                act_play.SetBool("Attack", false);
                act_play.SetBool("Defend", true);
                Attack.text = "";
                Defend.text = "Defend";
                timer = 0;
            }
        }
    }

    public void Go_Main()
    {
        SceneManager.LoadScene("Main_Scene");
    }
}
