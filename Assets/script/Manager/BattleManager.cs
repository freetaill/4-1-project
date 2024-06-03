using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Unity.VisualScripting;
using UnityEngine.Android;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Android;

public class BattleManager : MonoBehaviour
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
    public GameObject Attack_range;

    [Header("Move")]
    public float moveSpeed;
    Vector3 moveVec;
    public FixedJoystick joystick;

    public Text Timer;
    float timer;

    float Act_Waitingtime;
    float Act_timer;
    public float gyro_x;

    public GameObject[] Monster;
    public GameObject Monsterlist;
    public GameObject[] M_Spawnpoint;
    bool Spawn_flag;
    float Mob_delay;
    float Mob_time;

    int Gold;

    private void Start()
    {
        Screen.orientation = ScreenOrientation.LandscapeLeft;
        player = player.GetComponent<Rigidbody>();
        act_play = act_play.GetComponent<Animator>();
        Act_timer = 1.0f;
        Act_Waitingtime = 1.0f;
        Mob_time= 0.5f;
        Mob_delay = 0.5f;
    }


    // Update is called once per frame
    void FixedUpdate()
    {
        if (!UnityEngine.InputSystem.Gyroscope.current.enabled)
        {
            Debuglog.text = "Error";
            InputSystem.EnableDevice(UnityEngine.InputSystem.Gyroscope.current);
        }
        if(true)
        {
            Act_timer += Time.deltaTime;
            Mob_time += Time.deltaTime;
            timer += Time.deltaTime;
            Gold = GameManager.instance.player.Read_coin();
            GoldText.text = Gold.ToString();
            gyro_x = UnityEngine.InputSystem.Gyroscope.current.angularVelocity.x.ReadValue();
            gyroText_x.text = gyro_x.ToString();
            animator_fight(gyro_x);
            Monster_Spown();
        }
    }

    private void LateUpdate()
    {
        //act_play.SetBool("Attack", false);
        //act_play.SetBool("Defend", false);
    }

    void animator_fight(float Gyro_x)
    {
        if (Act_timer >= Act_Waitingtime)
        {
            Attack_range.SetActive(false);
            if (Gyro_x < -1)
            {
                act_play.SetBool("Attack", true);
                act_play.SetBool("Defend", false);

                //�÷��̾� ���� ���� on / off
                Attack_range.SetActive(true);

                Attack.text = "Attack";
                Defend.text = "";
                Act_timer = 0; 
            }
            else if (Gyro_x > 1)
            {
                act_play.SetBool("Attack", false);
                act_play.SetBool("Defend", true);

                Attack.text = "";
                Defend.text = "Defend";
                Act_timer = 0;
            }
        }
    }

    void Monster_Spown()
    {
        int M_len = Monster.Length;
        int M_rand = Random.Range(0, M_len);

        int S_len = M_Spawnpoint.Length;
        int S_rand = Random.Range(0, S_len);

        if(Monsterlist.transform.childCount == 20)
        {
            Spawn_flag = false;
        }
        else if(Monsterlist.transform.childCount == 0)
        {
            Spawn_flag = true;
        }

        //���� ��ȯ ���� ����Ʈ �����Ͽ� �� ����Ʈ�� ���� �ð����� ���� ��ȯ
        if (Spawn_flag && Mob_time > Mob_delay)
        {
            //���� ������ ��ũ��Ʈ �ο�
            GameObject temp = Instantiate(Monster[M_rand], M_Spawnpoint[S_rand].transform.position, Quaternion.identity);
            temp.transform.parent = Monsterlist.transform;
            temp.AddComponent<Monster>();
            Mob_time = 0;
        }
    }

    public void Go_Main()
    {
        GameManager.instance.save();
        SceneManager.LoadScene("Main_Scene");
    }
}