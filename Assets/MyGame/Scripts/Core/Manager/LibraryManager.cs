using System;
using System.Linq;
using Sirenix.OdinInspector;
using UnityEngine;
using Random = UnityEngine.Random;

public class LibraryManager : MonoBehaviour
{
    #region Property
    
    public NewLibrarySystem LibrarySystem{ get; private set; }

    #endregion

    #region Const

    private const string DATA_SETTINGS_PATH = "TD/ScriptableObjects/DataSettings";

    #endregion

    private void Awake() => Initialize();

    #region Initialize
    
    private void Initialize(){

        LibrarySystem = new NewLibrarySystem(Resources.Load<DataSettings>(DATA_SETTINGS_PATH));
    }

    #endregion

    [ContextMenu("Print Random TowerData")]
    public void ShowAnyTowerData(){
        var towerNames = Enum.GetValues(typeof(TowerName)).Cast<TowerName>().ToList();
        var towerName  = towerNames[Random.Range(0, towerNames.Count)];
        Debug.Log(LibrarySystem.GetData<NewTowerData>(towerName.ToString()));
    }
    [ContextMenu("Print Random EnemyData")]
    public void ShowAnyEnemyData(){
        var enemyNames = Enum.GetValues(typeof(EnemyName)).Cast<EnemyName>().ToList();
        var enemyName = enemyNames[Random.Range(0, enemyNames.Count)];
        Debug.Log(LibrarySystem.GetData<NewEnemyData>(enemyName.ToString()));
    }
    [ContextMenu("Print Random ItemData")]
    public void ShowAnyItemData(){
        var itemNames = Enum.GetValues(typeof(ItemName)).Cast<ItemName>().ToList();
        var itemName = itemNames[Random.Range(0, itemNames.Count)];
        Debug.Log(LibrarySystem.GetData<NewItemData>(itemName.ToString()));
    }
}
