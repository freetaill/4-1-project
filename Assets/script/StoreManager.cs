using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class StoreManager : MonoBehaviour
{
    public Transform ContentContainer;
    private List<ItemData> storeItems;
    // Start is called before the first frame update
    void Start()
    {

    }
    void LoadStoreData(){
      storeItems = new List<ItemData>
      {
         new ItemData("LightSaber", 100, Resources.Load<Sprite>("LightSaber"))
      };
    }
}