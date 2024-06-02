using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class OptionManager : MonoBehaviour
{
     public GameObject _popup;
     public GameObject _exitPopup;
     public Animator anim;
     
    // Start is called before the first frame update
    void Start()
    {
        exitGame();
        transform.localScale = Vector3.one * 0.1f;
      
        _popup.SetActive(false); // 시작할때 팝업 비활성화
        _exitPopup.SetActive(false);
    }
    public void openOption(){
        anim = _popup.GetComponent<Animator>();
        _popup.SetActive(true);
        anim.Play("OpenPopUp");
        Time.timeScale = 0f;
     }
     public void closeOption(){
        _popup.SetActive(false);
        Time.timeScale = 1f;
     }
     public void exitGame(){
        if (Application.platform == RuntimePlatform.Android)
        {
            if (Input.GetKey(KeyCode.Escape))
            {
                _exitPopup.SetActive(true);
            }
        }
     }
     public void exitCancel(){
        _exitPopup.SetActive(false);
     }
     public void exit(){
        Application.Quit();
     }
}
