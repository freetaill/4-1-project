using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.IO;

public class StoreManager : MonoBehaviour
{
    public Transform ContentContainer;
    private StoreData storeData;

    public GameObject mainUI;
    public GameObject weaponUI;
    public GameObject shieldUI;
    // Start is called before the first frame update
    void Start()
    {

    }
    public StoreData LoadStoreData(){
        TextAsset jsonFile = Resources.Load<TextAsset>("Data/storeData");
        if (jsonFile != null)
        {
            string json = jsonFile.text;
            storeData = JsonUtility.FromJson<StoreData>(json);

            // // 스프라이트 로드
            // foreach (var item in storeItems)
            // {
            //     item.sprite = LoadSprite(item.spritePath);
            // }
            return storeData;

        }
        else
        {
            Debug.LogError("Cannot find file!!");
            return storeData;
        }
    }

    private Sprite LoadSprite(string path)
    {
        // Resources 폴더에서 스프라이트 로드
        return Resources.Load<Sprite>(path);
    }
    // void SaveStoreData(){

    // }
    void clickButton(int index){
        if(index == 0){
            
        }
    }
}
