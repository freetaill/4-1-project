using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


public class ScrollViewManager : MonoBehaviour
{
    public Transform content;
    public GameObject prefab;
    public List<StoreItem> items;
    public StoreManager storeManager;
    // Start is called before the first frame update
    void Start()
    {
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
        foreach (var item in items){ //StoreManger?¥ê??? ??????? ????.
            GameObject newItem = Instantiate(prefab);
            Merchandise merchan = newItem.GetComponent<Merchandise>();
            merchan.Init(i, item);
            newItem.transform.SetParent(content, false);
            i++;
        }

    }
}