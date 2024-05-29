using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


public class ScrollViewManager : MonoBehaviour
{
    public Transform content;
    public GameObject prefab;
    // Start is called before the first frame update
    void Start()
    {
        prefab = Resources.Load("Item") as GameObject;
    }

    void UpdateScrollView(List<ItemData> items) // 스크롤뷰 업데이트
    {
        int i = 0;
        foreach (var item in items){ //StoreManger로부터 데이터를 받아옴.
            GameObject newItem = Instantiate(prefab);
            Merchandise merchan = newItem.GetComponent<Merchandise>();
            merchan.Init(i, item);
            newItem.transform.SetParent(content, false);
            i++;
        }

    }
}
