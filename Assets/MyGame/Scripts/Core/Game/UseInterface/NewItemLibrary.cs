using System.Collections.Generic;
using UnityEngine;

public class NewItemLibrary : ILibrary
{
    private Dictionary<string, NewItemData> _itemLibrary;

    public NewItemLibrary(){ _itemLibrary = new Dictionary<string, NewItemData>(); }
    public NewItemLibrary(NewItemData[] itemDatas) : this(){
        foreach(var item in itemDatas){
            _itemLibrary.Add(item.dataName, item);
        }
    }

    public void AddData(string name, dynamic data){
        if(data is NewItemData){
            var itemData = data as NewItemData;
            _itemLibrary.Add(itemData.dataName, itemData);
        }else{
            Debug.LogWarning($"{data} is not a NewItemData");
        }
    }

    public dynamic GetData(){
        return _itemLibrary;
    }

    public dynamic GetData(string name)
    {
        throw new System.NotImplementedException();
    }
}
