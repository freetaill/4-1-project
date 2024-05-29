using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Unity;


public class Merchandise : MonoBehaviour
{
    public int index;
    public Image merch_img;
    public Text desc;

    void Awake(){
        merch_img = transform.Find("MerchandiseImage").GetComponent<Image>();
        //컴포넌트 추가시 코드도 추가.
    }


    public void Init(int index,ItemData item){
        this.index = index;
        merch_img.sprite = item.img;
    }

    
    
}
