using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

// [Serializable]
public class LibrarySystem : ILibrarySystem
{

    #region Property
    public Dictionary<string, NewLibrary<InstantiateData>> Libraries{ get; private set; }
    [ShowInInspector]
    public Dictionary<string, ILibrary> LibrariesForInterface{ get; private set; }
    #endregion

    public Dictionary<LibraryName, IDictionary> TestLibrary;

    #region Const
    [ShowInInspector]
    private const string NEWITEMDATA_PATH = "TD/ScriptableObjects/ItemDatas";
    [ShowInInspector]
    private const string NEWTOWERDATA_PATH = "TD/ScriptableObjects/TowerDatas";
    [ShowInInspector]
    private const string NEWENEMYDATA_PATH = "TD/ScriptableObjects/EnemyDatas";
    #endregion

    public LibrarySystem(){
        Initialize();
        Initialize_Interface();

        // TestLibrary.Add(LibraryName.Tower, new Dictionary<TowerName, NewTowerData>());
        // var libary = TestLibrary[LibraryName.Tower];
        // libary.Add(TowerName.Dude, new NewTowerData());
    }

    public dynamic AddData(string libraryLabel, InstantiateData data){
        if( !Libraries.ContainsKey(libraryLabel) ){
            if(Libraries.TryAdd(libraryLabel, new NewLibrary<InstantiateData>())){
                Debug.Log($"Create a new Library {libraryLabel} succeed!");
            }
        }
        Libraries[libraryLabel].Add(data);
        return Libraries[libraryLabel][data.dataName];
    }
    public void AddLibrary(string libraryLabel, NewLibrary<InstantiateData> library){
        if( !Libraries.ContainsKey(libraryLabel) ){
            Libraries.Add(libraryLabel, library);
        }else{ Debug.Log($"Library {libraryLabel} exists."); }
    }
    // public NewLibrary<InstantiateData> GetLibrary( string libraryLabel ) => Libraries[libraryLabel];

    #region Initialize    
    private void Initialize(){
        Libraries = new Dictionary<string, NewLibrary<InstantiateData>>();
        var itemDatas = Resources.LoadAll<NewItemData>( NEWITEMDATA_PATH );
        var towerDatas = Resources.LoadAll<NewTowerData>( NEWTOWERDATA_PATH );
        var enemyDatas = Resources.LoadAll<NewEnemyData>( NEWENEMYDATA_PATH );
        // _itemLibrary = new NewLibrary<NewItemData>( itemDatas );
        // _towerLibrary = new NewLibrary<NewTowerData>( towerDatas );
        // _enemyLibrary = new NewLibrary<NewEnemyData>( enemyDatas );
        AddLibrary(nameof(NewItemData), new NewLibrary<InstantiateData>(itemDatas));
        AddLibrary(nameof(NewTowerData), new NewLibrary<InstantiateData>(towerDatas));
        AddLibrary(nameof(NewEnemyData), new NewLibrary<InstantiateData>(enemyDatas));

        // var dataSettings = Resources.Load<DataSettings>("TD/ScriptableObjects/DataSettings");
        // dataSettings.ItemSettings.Items
    }
    #endregion

    // ---- For Interface ----
    private void Initialize_Interface(){
        var itemDatas = Resources.LoadAll<NewItemData>( NEWITEMDATA_PATH );
        var towerDatas = Resources.LoadAll<NewTowerData>( NEWTOWERDATA_PATH );
        var enemyDatas = Resources.LoadAll<NewEnemyData>( NEWENEMYDATA_PATH );
        LibrariesForInterface = new Dictionary<string, ILibrary>{
            { nameof(NewItemData), new NewItemLibrary() },
            { nameof(NewTowerData), new NewTowerLibrary() },
            { nameof(NewEnemyData), new NewEnemyLibrary(enemyDatas) }
        };
    }

    public ILibrary GetLibrary(string libraryName){
        return LibrariesForInterface[libraryName];
    }

    public dynamic GetData(string libraryName, string dataName){
        return LibrariesForInterface[libraryName].GetData(dataName);
    }

}

public interface ILibrarySystem
{
    public ILibrary GetLibrary(string libraryName);
    public dynamic GetData(string libraryName, string dataName);
}
public enum LibraryName
{
    None,
    Tower,
    Enemy,
    Item
}