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
    public Text itemPriceTxt;
    public ScrollViewManager scm;

    public int itemPrice;

    void Awake(){
        merch_img = transform.Find("MerchandiseImage").GetComponent<Image>();
        itemName = transform.Find("ItemName").GetComponent<Text>();
        itemPriceTxt = transform.Find("ItemPrices").GetComponent<Text>();
        scm = GameObject.Find("Scroll View").GetComponent<ScrollViewManager>();
    }


    public void Init(int index,StoreItem item){
        this.index = index;
        merch_img.sprite = Resources.Load<Sprite>(item.spritePath);
        itemName.text=item.itemName;
        itemPrice = item.itemPrice;
        itemPriceTxt.text= string.Format("{0:#,###}",itemPrice)+"P";
        print(itemPrice);   
    }
    public void onClickBtn(){
        scm.Purchase(this);
    }
    
    
}   