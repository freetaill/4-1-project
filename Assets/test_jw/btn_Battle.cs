using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class btn_Battle : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void Go_Main()
    {
        SceneManager.LoadScene("Main_Scene");
    }
}
