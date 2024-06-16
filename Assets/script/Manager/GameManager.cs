using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public interface ILoader<Key, Value>
{
    Dictionary<Key, Value> MakeDict();
}

[System.Serializable]
public class Player
{
    public string name = "defo";
    public int top = 0;

    public int total_steps = 0;
    public int[] Daily_steps = Enumerable.Repeat<int>(0, 100).ToArray<int>();

    public double total_lengths = 0;
    public double[] Daily_lengths = Enumerable.Repeat<double>(0.0f, 100).ToArray<double>();

    public int level = 1;
    public int exp = 0;
    public int coins = 0;

    // [0]: 공격력, [1]: 체력, [2]: 방어력
    public int[] status = Enumerable.Repeat<int>(1, 3).ToArray<int>();

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
        Daily_steps[top] += steps;
        total_steps += steps;
        level = 1 + total_steps / 1000;
        exp = total_steps - (level - 1) * 1000;
    }

    public int Read_steps() { return Daily_steps[top]; }

    public void Get_lengths(double lengths)
    {
        Daily_lengths[top] = lengths;
    }

    public double Read_length() { return Daily_lengths[top]; }

    public int Read_level() { return level; }

    public int Read_exp() { return exp; }

    //재화 관리
    public void Add_coin(int Add_coins)
    {
        coins += Add_coins;
    }

    public void Decrease_coin(int coinsf)
    {
        coins -= coinsf;
    }

    public int Read_coin() { return coins; }

    // 캐릭터 능력치 관리
    public void Write_status() { }

}

[System.Serializable]
public class StoreData
{
    public List<StoreItem> items;
}

[System.Serializable]
public class StoreItem
{
    public string itemName;
    public int itemID;
    public int itemPrice;
    public string desc;

    public string spritePath;
}

public class GameManager : MonoBehaviour
{

    public static GameManager instance;

    public Player player = new Player();

    string path;

    public Text output;

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

        path = Application.persistentDataPath;
    }

    [ContextMenu("To Json Data")]
    public void save()
    {
        string data = JsonUtility.ToJson(player);
        File.WriteAllText(path + "/Playerdata.json", data);
        //File.WriteAllText("C:\\Users\\prl41\\Downloads\\4-1-project\\Assets\\test_jw" + "/save01.json", data);
    }

    public void load()
    {
        if (File.Exists(path + "/Playerdata.json"))
        {
            var data = File.ReadAllText(path + "/Playerdata.json");
            player = JsonUtility.FromJson<Player>(data);

            output.text = "clear";
        }
        else
        {
            save();
        }
    }

    public void DataClear()
    {
        player = new Player();
        File.Delete(path + "/Playerdata.json");
    }
}