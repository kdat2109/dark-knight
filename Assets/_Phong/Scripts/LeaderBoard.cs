using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using UnityEngine;

public class LeaderBoard : MonoBehaviour
{
    public static LeaderBoard Instance;
    public string account;
    
    public GameObject leaderBoardItemPrefab;
    public Transform leaderBoardContentParent;
    
    
    
    [Serializable]
    public class Data
    {
        public string account;
        public int wave;
    }
    
    public List<Data> data = new List<Data>();

    void Awake()
    {
        //LoadData();
        Instance = this;
    }

    public void AddData(string account, int wave = 0)
    {
        for (int i = 0; i < data.Count; i++)
        {
            if (data[i].account == account)
            {
                if (wave > data[i].wave)
                {
                    data[i].wave = wave;
                    //SaveData();
                }
                return;
            }
        }
        
        data.Add(new Data() { account = account, wave = wave });
        //SaveData();
    }

    public void SaveData()
    {
        string dataToString = JsonConvert.SerializeObject(data);
        
        var savePath = Application.persistentDataPath + "/data.json";
        
        Debug.Log(savePath);
        File.WriteAllText(savePath, dataToString);
    }

    public void LoadData()
    {
        var loadPath = Application.persistentDataPath + "/data.json";
        Debug.Log("Load : "+loadPath);
        
        if(!File.Exists(loadPath))
            return;
        
        var json = File.ReadAllText(loadPath);
        
        data = JsonConvert.DeserializeObject<List<Data>>(json);
        if (data == null)
        {
            data = new List<Data>();
        }
    }


    public void showLeaderBoardUI()
    {
        foreach (Transform child in leaderBoardContentParent)
        {
            Destroy(child.gameObject);   
        }
        
        data.Sort((a, b) => b.wave.CompareTo(a.wave));

        foreach (var d in data)
        {
            GameObject go = Instantiate(leaderBoardItemPrefab, leaderBoardContentParent);
            LeaderBoardUI item =  go.GetComponent<LeaderBoardUI>();
            item.SetData(d.account, d.wave + 1);
        }
    }

}