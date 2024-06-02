using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.Android;

public class ExerciseManager : MonoBehaviour
{
    [Header("Text")]
    public Text stepsText;
    public Text GoldText;

    [Header("character")]
    public Rigidbody player;
    public Animator act_play;

    [Header("Object")]
    public GameObject Map;
    public GameObject Ground;

    [Header("Move")]
    public float moveSpeed;

    delegate IEnumerator exer_mode(double speed);
    GpsLocation GpsLocation;
    bool isUpdating;
    double Player_move_speed = 1.6d;

    public Text GPSOut;
    public Text GPSLength;
    public Text GPSUpdateLength;

    float Waitingtime;
    float timer;
    int modes = 0;
    int FirstStep;
    int post_count = 0;
    int nowGenvec = 0;
    int count;
    int Gold;

    // Start is called before the first frame update
    void Start()
    {
        //FirstStep = StepCounter.current.stepCounter.ReadValue();
        count = 0;
        timer = 1.0f;
        Waitingtime = 1.0f;
        moveSpeed= 10.0f;
        GpsLocation = GetComponent<GpsLocation>();
        Exercise_mode(modes);
        GameObject.Instantiate(Map, new Vector3(0, 0, nowGenvec), Quaternion.identity).transform.parent = Ground.transform;
    }

    private void Update()
    {
        Generate_Map();
        if (!LinearAccelerationSensor.current.enabled)
        {
            Debug.Log("The Device is not enabled");
            InputSystem.EnableDevice(LinearAccelerationSensor.current);
        }

        if (!Permission.HasUserAuthorizedPermission(Permission.FineLocation))
        {
            Permission.RequestUserPermission(Permission.FineLocation);
            Permission.RequestUserPermission(Permission.CoarseLocation);
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
                GameManager.instance.player.Get_steps(count);
                stepsText.text = count.ToString();
                GoldText.text = GameManager.instance.player.Read_coin().ToString();
                GPSLength.text = GameManager.instance.player.Read_length().ToString();
                Move();
                if (post_count != count && !isUpdating)
                {
                    GpsLocation.StartCoroutine(GpsLocation.Getlocation_Run(Player_move_speed));
                    isUpdating = !isUpdating;
                    post_count = count;
                }
                Generate_Map();
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
            player.transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);
            //player.AddForce(Vector3.forward * moveSpeed * Time.deltaTime, ForceMode.VelocityChange);
        }
    }

    void Generate_Map()
    {
        //Debug.Log(player.gameObject.transform.position.z.ToString() + ":" + nowGenvec.ToString());
        if (nowGenvec <= player.gameObject.transform.position.z)
        { 
            nowGenvec += 940;
            GameObject.Instantiate(Map, new Vector3(0, 0, nowGenvec), Quaternion.identity).transform.parent = Ground.transform;
        }
        if (Ground.transform.childCount >= 3)
        {
            GameObject obj = Ground.transform.GetChild(0).gameObject;
            Destroy(obj);
        }
    }

    void Exercise_mode(int mode)
    {
        if(mode == 0)
        {
            //걷기
            Player_move_speed = 1.6d;
        }
        else if(mode == 1)
        {
            //달리기
            Player_move_speed = 2.1d;
        }
        else if (mode == 2)
        {
            // 자전거
            Player_move_speed = 4d;
        }
    }

    public void Go_Main()
    {
        GameManager.instance.save();
        SceneManager.LoadScene("Main_Scene");
    }
}
