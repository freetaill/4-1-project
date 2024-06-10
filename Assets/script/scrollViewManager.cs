using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


public class ScrollViewManager : MonoBehaviour
{
    public Transform content;
    public GameObject prefab;
    public List<StoreItem> items;
    public StoreManager storeManager;
    public Text hasCoin;

    public AudioSource[] buySound;
    // Start is called before the first frame update
    void Start()
    {
        buySound = GameObject.Find("AudioSource").GetComponents<AudioSource>();
        prefab = Resources.Load("Item") as GameObject;
        content = transform.GetChild(0).GetChild(0);
        storeManager = GameObject.Find("StoreManager").GetComponent<StoreManager>();
        UpdateScrollView(storeManager.LoadStoreData());
    }

    void UpdateScrollView(StoreData storedata) // ?????? ???????
    {
        print("see");
        items = storedata.items;
        int i = 0;
        foreach (var item in items){ //StoreManger?κ??? ??????? ????.
            GameObject newItem = Instantiate(prefab);
            Merchandise merchan = newItem.GetComponent<Merchandise>();
            merchan.Init(i, item);
            newItem.transform.SetParent(content, false);
            i++;
        }
        hasCoin.text = GameManager.instance.player.coins.ToString();

    }
    public void Purchase(Merchandise merch){
        Debug.Log(merch.itemName.text.ToString());
        print(GameManager.instance.player.coins);

        if(CheckCoin(GameManager.instance.player.coins, (int)merch.itemPrice))
        {
            if(merch.itemName.text.ToString() == "전설의 검"){
                GameManager.instance.player.status[0] += 10; //공격력 증가
                Debug.Log("공격력 증가");
            }
            else if(merch.itemName.text.ToString() == "전설의 방패")
                GameManager.instance.player.status[2] += 10; //방어력 증가
            else if(merch.itemName.text.ToString() == "전설의 신발")
                GameManager.instance.player.status[2] += 20; //방어력 증가
            buySound[0].Play();
        }
        else{
            buySound[1].Play();
        }
    }
    public bool CheckCoin(int totalGold, int itemPrice){
        if(totalGold < itemPrice)
            return false;
        else   
            return true;
    }
}

