using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public static class SaveSystem
{
    public static string SaveDirectory{ get; set; }

    public static void Save(SaveData data, string saveSlot){
        if(data == null){
            throw new SaveDataNullException("SaveData is Null, should not be Null!");
        }

        string jsonData = JsonUtility.ToJson(data);

        string fullPath = Path.Combine(Application.persistentDataPath, SaveDirectory, saveSlot+".sav");
        string dir      = Path.GetDirectoryName(fullPath);
        if(!Directory.Exists(dir)){ Directory.CreateDirectory(dir); }

        File.WriteAllText(fullPath, jsonData);
        Debug.Log("Save Success!");
    }
    public static void Load(string saveSlot){
        string fullPath = Path.Combine(Application.persistentDataPath, SaveDirectory, saveSlot+".sav");
        if(File.Exists(fullPath)){
            string   jsonData = File.ReadAllText(fullPath);
            SaveData data     = JsonUtility.FromJson<SaveData>(jsonData);
            GameManager.Instance.newPlayer.LoadData(data);
        }
    }
}

[Serializable]
public class SaveData{
    public string name;
    public int    maxHp;
    public int    currentHp;
    public int    money;
    public List<string> ownedTower;
    public List<int>    clearedStage;

    public string dateTime = DateTime.Now.ToString();
}