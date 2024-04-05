using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class characterStat : MonoBehaviour
{
    public int curMoney;
    public int getMoney;
    [SerializeField]Text moneyText;

    AudioSource coinGet;
    // Start is called before the first frame update
    void Start()
    {
        curMoney = 0;
        getMoney = 0;
        coinGet = gameObject.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        IncreaseMoney();
    }
    void IncreaseMoney(){
        if(getMoney>0){
            curMoney += getMoney;
            moneyText.text = curMoney.ToString();
            getMoney = 0;
            coinGet.Play();
        }
    }
}
