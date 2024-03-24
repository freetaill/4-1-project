using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.Android;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Android;

using UnityEngine.UI;

public class test : MonoBehaviour
{
    [SerializeField]
    public int count = 0;
    [SerializeField]
    public Text stepsText;
    [SerializeField]
    public Text accelText;
    [SerializeField]
    public Text initText;
    [SerializeField]
    public AndroidStepCounter asc;
    [SerializeField]
    public Text DebugText;
    int FirstStep;
    int initStep = 0;
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
        initText.text = FirstStep.ToString();
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
        FirstStep = StepCounter.current.stepCounter.ReadValue();
    }
 
 
    // Update is called once per frame
    void FixedUpdate()
    {
        if (!LinearAccelerationSensor.current.enabled)
        {
            accelText.text = "The Device is not enabled";
            InputSystem.EnableDevice(LinearAccelerationSensor.current);
            //DebugText.text += "Linear Acceleration was re-enabled!!\n";
        }
        else
        {
            //DebugText.text += "Currently Enabled: " + LinearAccelerationSensor.current.ToString() + "\n";
            accelText.text = LinearAccelerationSensor.current.acceleration.ReadValue().ToString();
        }
 
        if (!StepCounter.current.enabled)
        {
            //initText.text = "Stepcounter is Paused";
            InputSystem.EnableDevice(StepCounter.current);
            //DebugText.text += "Linear Acceleration was re-enabled!!\n";
        }
        else
        {
            //DebugText.text += "Currently Enabled: " + StepCounter.current.ToString() + "\n";
            if(FirstStep != 0){
                count = StepCounter.current.stepCounter.ReadValue()-FirstStep;
                stepsText.text = "Steps: " + count.ToString();
            }
            else{
                getCount();
            }
        }
    }

}