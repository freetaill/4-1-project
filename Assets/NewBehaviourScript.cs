using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Android;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Android;

using UnityEngine.UI;

 

public class NewBehaviourScript : MonoBehaviour
{
     [SerializeField]
    public Text stepsText;
    private int lastStepCount = 0;

    void Start()
    {
        if (!Permission.HasUserAuthorizedPermission("android.permission.ACTIVITY_RECOGNITION"))
        {
            Permission.RequestUserPermission("android.permission.ACTIVITY_RECOGNITION");
        }
        // StepCounter를 활성화합니다.
        if (StepCounter.current != null)
        {
            InputSystem.EnableDevice(StepCounter.current);
        }
        else
        {
            Debug.LogWarning("StepCounter is not available on this device.");
        }
    }

    void Update()
    {
        // StepCounter에서 현재 걸음 수를 읽습니다.
        if (StepCounter.current != null)
        {
            int currentStepCount = StepCounter.current.stepCounter.ReadValue();
            // 걸음 수에 변화가 있을 경우, 이를 로그로 출력합니다.
            if (currentStepCount != lastStepCount)
            {
                Debug.Log($"Step Count: {currentStepCount}");

                lastStepCount = currentStepCount;
                stepsText.text = "Steps: " + lastStepCount.ToString();
            }
        }
    }
}
