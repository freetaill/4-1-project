using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class Player
{
    string name;
    int steps = 0;
    int level = 1;
    int exp = 0;
    int Gold = 0;

    public void insert_Data(int Get_steps)
    {
        steps = Get_steps;
        //int x = steps / level;
        //level = (int)System.Math.Log(level) + 100;
    }
    public void insert_coin(int Get_coins)
    {
        Gold += Get_coins;
    }

    public int Get_coin()
    {
        return Gold;
    }
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
        //教臂沛 积己
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(this.gameObject);
        }
        DontDestroyOnLoad(gameObject);
        //教臂沛 积己

        path = Application.dataPath + "/save";
    }

    /*public void save()
    {
        string data = JsonUtility.ToJson(nowPlayer) + "/" + JsonUtility.ToJson(nowAnimal) + "/"
            + JsonUtility.ToJson(nowranking);
        File.WriteAllText(path + nowSlot.ToString(), data);
    }

    public void load()
    {
        string data = File.ReadAllText(path + nowSlot.ToString());
        string[] datasplit = data.Split('/');
        nowPlayer = JsonUtility.FromJson<Player>(datasplit[0]);
        nowAnimal = JsonUtility.FromJson<Animal>(datasplit[1]);
        nowranking = JsonUtility.FromJson<Ranking>(datasplit[2]);
    }

    public void DataClear()
    {
        nowSlot = -1;
        nowPlayer = new Player();
    }*/
}
