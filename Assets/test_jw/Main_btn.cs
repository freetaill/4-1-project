using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Main_btn : MonoBehaviour
{
    private void Start()
    {
        
    }

    public void Go_Exercise()
    {
        SceneManager.LoadScene("Exercise_Scene");
    }

    public void Go_Battle ()
    {
        SceneManager.LoadScene("Battle_Scene");
    }
}
