using System.Collections.Generic;
using UnityEngine;

public class NewEnemyLibrary : ILibrary
{
    private Dictionary<string, NewEnemyData> _enemyLibrary;

    public NewEnemyLibrary(){ _enemyLibrary = new Dictionary<string, NewEnemyData>(); }
    public NewEnemyLibrary(NewEnemyData[] enemyDatas) : this(){
        foreach(var enemy in enemyDatas){
            _enemyLibrary.Add(enemy.dataName, enemy);
        }
    }

    public void AddData(string name, dynamic data){
        if(data is NewEnemyData){
            var enemyData = data as NewEnemyData;
            _enemyLibrary.Add(enemyData.dataName, enemyData);
        }else{
            Debug.LogWarning($"{data} is not a NewEnemyData");
        }
    }

    public dynamic GetData(){
        return _enemyLibrary;
    }

    public dynamic GetData(string name)
    {
        throw new System.NotImplementedException();
    }
}
