using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Android;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class MainManager : MonoBehaviour
{
    private void Start()
    {
        Screen.orientation = ScreenOrientation.Portrait;

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

        if (!UnityEngine.InputSystem.Gyroscope.current.enabled)
        {
            InputSystem.EnableDevice(UnityEngine.InputSystem.Gyroscope.current);
        }

        if (!Permission.HasUserAuthorizedPermission(Permission.FineLocation))
        {
            Permission.RequestUserPermission(Permission.FineLocation);
            Permission.RequestUserPermission(Permission.CoarseLocation);
        }

        GameManager.instance.load();
        GameManager.instance.save();

    }

    public void Go_Exercise()
    {
        SceneManager.LoadScene("Exercise_Scene");
    }

    public void Go_Battle ()
    {
        SceneManager.LoadScene("Battle_Scene");
    }

    public void Go_Store ()
    {
        SceneManager.LoadScene("Store_Scene");
    }
}
