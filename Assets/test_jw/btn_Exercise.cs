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
    public Animator act_play;

    [Header("Object")]
    public GameObject Map;
    public GameObject Ground;

    [Header("Move")]
    public float moveSpeed;

    float Waitingtime;
    float timer;
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
                GoldText.text = GameManager.instance.player.Read_coin().ToString();
                Move();
                post_count = count;
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
            // 
            //player.transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);
            player.AddForce(Vector3.forward * moveSpeed * Time.deltaTime, ForceMode.VelocityChange);
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

    public void Go_Main()
    {
        SceneManager.LoadScene("Main_Scene");
    }
}
