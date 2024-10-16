using System.Collections.Generic;
using UnityEngine;

public class ItemLibrary : MonoBehaviour
{   
    #region Field
    [SerializeField] private List<ItemData> items;
    [SerializeField] private Dictionary<int, ItemData> itemDictionary = new Dictionary<int, ItemData>();
    #endregion

    #region Property
    public ItemData this[int id] => itemDictionary[id];
    #endregion

    private void Awake(){
        foreach( var item in items ){
            itemDictionary.Add( item.id, item );
        }
    }

}
