using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class Player
{
    string name;

    int total_steps = 0;
    int top = 0;
    int[] Daily_steps = new int[100];

    int level = 1;
    int exp = 0;
    int coins = 0;

    // [0]: 공격력, [1]: 체력, [2]: 방어력
    int[] status = new int[3];

    //세이브 불러오기
    public void insert_Data(int Get_steps)
    {
        Daily_steps[top] += Get_steps;
        //int x = steps / level;
        //level = (int)System.Math.Log(level) + 100;
    }

    // 걸음 수, 레벨 관리
    public void Get_steps(int steps)
    {
        Daily_steps[top] = steps;
    }

    public int Read_steps() { return Daily_steps[top]; }

    public int Read_level() { return level; }

    public int Read_exp() { return exp; }

    //재화 관리
    public void Add_coin(int Add_coins)
    {
        coins += Add_coins;
    }

    public int Read_coin() { return coins; }

    // 캐릭터 능력치 관리
    public void Write_status() { }

    //캐릭터 아이템 관리
    public void Get_items(Item item) { }

}

public class Item
{
    int id;
    string name;
}

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public Player player = new Player();

    private int steps = 0;
    private int exe = 0;

    string path;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(this.gameObject);
        }
        DontDestroyOnLoad(gameObject);

        path = Application.dataPath + "/save";
    }

    public void save()
    {
        string data = JsonUtility.ToJson(player);
        File.WriteAllText(path + "save", data);
    }

    public void load()
    {
        string data = File.ReadAllText(path);
        string[] datasplit = data.Split('/');
        player = JsonUtility.FromJson<Player>(datasplit[0]);
    }

    public void DataClear()
    {
        player = new Player();
    }
}