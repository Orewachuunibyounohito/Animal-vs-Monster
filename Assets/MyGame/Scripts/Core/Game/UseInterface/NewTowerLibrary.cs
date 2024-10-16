using System.Collections.Generic;
using UnityEngine;

public class NewTowerLibrary : ILibrary
{
    private Dictionary<string, NewTowerData> _towerLibrary;

    public NewTowerLibrary(){ _towerLibrary = new Dictionary<string, NewTowerData>(); }
    public NewTowerLibrary(NewTowerData[] towerDatas) : this(){
        foreach(var tower in towerDatas){
            _towerLibrary.Add(tower.dataName, tower);
        }
    }

    public void AddData(string name, dynamic data){
        if(data is NewTowerData){
            var towerData = data as NewTowerData;
            _towerLibrary.Add(towerData.dataName, towerData);
        }else{
            Debug.LogWarning($"{data} is not a NewTowerData");
        }
    }

    public dynamic GetData(){
        return _towerLibrary;
    }

    public dynamic GetData(string name)
    {
        throw new System.NotImplementedException();
    }
}
