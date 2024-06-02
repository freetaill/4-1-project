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
    public Text itemPrice;

    void Awake(){
        merch_img = transform.Find("MerchandiseImage").GetComponent<Image>();
        itemName = transform.Find("ItemName").GetComponent<Text>();
        itemPrice = transform.Find("ItemPrice").GetComponent<Text>();
    }


    public void Init(int index,StoreItem item){
        this.index = index;
        merch_img.sprite = Resources.Load<Sprite>(item.spritePath);
        itemName.text=item.itemName;
        itemPrice.text=string.Format("{0:#,###}",item.itemPrice)+"P";
        print("hello");
    }

    
    
}