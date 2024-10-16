using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

[CreateAssetMenu( fileName = "NewReward", menuName = "TD/Reward Data" )]
public class RewardData : ScriptableObject
{
    #region Field
    [HideInInspector]
    [SerializeField] private List<ItemData>    _itemData    = new List<ItemData>();

    [InlineEditor]
    // [TableList, ReadOnly]
    [SerializeField] private List<NewItemData> _newItemData = new List<NewItemData>();
    [SerializeField] private int               _coins;
    #endregion

    #region Property
    public List<ItemData>    itemData    => _itemData;
    public List<NewItemData> newItemData => _newItemData;
    public int               Coins       => _coins;
    #endregion

    #region Public Methods
    public string ItemDetails(){
        string details = "";
        _itemData.ForEach( ( data ) => details += data.ToString()+"\n\n" );
        return details;
    }

    public string NewItemDetails(){
        string details = "";
        _newItemData.ForEach( ( data ) => details += data.ToString()+"\n\n" );
        return details;
    }
    #endregion
}
