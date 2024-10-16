using System.Collections.Generic;
using UnityEngine;

public class TowerLibrary : MonoBehaviour
{
    #region Field
    [SerializeField] private List<TowerData>               towerList;
    [SerializeField] private Dictionary<string, TowerData> allTowers = new Dictionary<string, TowerData>();
    #endregion

    #region Property
    public  Dictionary<string, TowerData> TowerDictionary        => allTowers;
    public  TowerData                     this[string towerName] => allTowers[towerName]; 
    #endregion

    void Awake(){
        foreach( var data in towerList ){
            allTowers.Add( data.towerPrefab.name, data );
        }
    }
}
