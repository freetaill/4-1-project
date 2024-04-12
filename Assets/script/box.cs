using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class box : MonoBehaviour
{
    GameObject player;
    bool isPlayerEnter, isCollided=false;
    [SerializeField]private Text gold;
    AudioSource breakBox;
    int goldAmount;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("player");
        isPlayerEnter = false;
        breakBox = this.gameObject.GetComponent<AudioSource>();
        goldAmount = Random.Range(100,500);
    }

    // Update is called once per frame
    void Update()
    {
        if (isPlayerEnter&&!isCollided){ 
            //gameObject.SetActive(false);
            player.GetComponent<characterStat>().getMoney += goldAmount;
            breakBox.Play();
            Destroy(gameObject, breakBox.clip.length);
            isCollided = true;
        }
    }
    void OnTriggerEnter(Collider other)
    {       
        if (other.gameObject == player)
        {            
            isPlayerEnter = true;           
        }
    }
    void OnTriggerExit(Collider other)
    {       
        if (other.gameObject == player)
        {
            isPlayerEnter = false;
        }
    }


}

