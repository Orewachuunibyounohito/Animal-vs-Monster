using System;
using System.Collections;
using System.Collections.Generic;

[Serializable]
public class NewLibrarySystem
{

    // [ShowInInspector]
    private IDictionary libraries;

    public NewLibrarySystem(DataSettings dataSettings){
        libraries = new Dictionary<object, object>();
        Assign(dataSettings.TowerSettings);
        Assign(dataSettings.EnemySettings);
        Assign(dataSettings.ItemSettings);
    }
    
    public TData GetData<TData>(object name) where TData : InstantiateData{
        IDictionary libObj = null;
        IDictionary<object, TData> library;
        switch(typeof(TData).ToString()){
            case nameof(NewTowerData): libObj = libraries[LibraryName.Tower] as IDictionary; break;
            case nameof(NewEnemyData): libObj = libraries[LibraryName.Enemy] as IDictionary; break;
            case nameof(NewItemData):  libObj = libraries[LibraryName.Item]  as IDictionary; break;
            default: throw new ArgumentException("Unrecognized Library!");
        }
        library = libObj.ValueCast<TData>();
        return library[name];
    }

    private void Assign(TowerDataSettings settings){
        libraries.Add(LibraryName.Tower, new Dictionary<object, object>());
        foreach(var dataSet in settings){
            ((IDictionary)libraries[LibraryName.Tower]).Add(dataSet.Name, dataSet.Data);
        }   
    }
    private void Assign(ItemDataSettings settings){
        libraries.Add(LibraryName.Item, new Dictionary<object, object>());
        foreach(var dataSet in settings){
            ((IDictionary)libraries[LibraryName.Item]).Add(dataSet.Name, dataSet.Data);
        }   
    }
    private void Assign(EnemyDataSettings settings){
        libraries.Add(LibraryName.Enemy, new Dictionary<object, object>());
        foreach(var dataSet in settings){
            ((IDictionary)libraries[LibraryName.Enemy]).Add(dataSet.Name, dataSet.Data);
        }   
    }
}
