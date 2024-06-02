using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Unity;


public class Merchandise : MonoBehaviour
{
    public int index;
    public Image merch_img;
    public Text itemName;

    void Awake(){
        merch_img = transform.Find("MerchandiseImage").GetComponent<Image>();
        itemName = transform.Find("ItemName").GetComponent<Text>();
    }


    public void Init(int index,StoreItem item){
        this.index = index;
        merch_img.sprite = Resources.Load<Sprite>(item.spritePath);
        itemName.text=item.itemName;
        print("hello");
    }

    
    
}