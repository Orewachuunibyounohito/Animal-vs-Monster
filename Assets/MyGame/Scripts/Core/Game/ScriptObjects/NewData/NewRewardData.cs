using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu( fileName = "NewReward", menuName = "TD/New Reward Data" )]
public class NewRewardData : ScriptableObject
{
    #region Field
    [SerializeField] private List<NewItemData> _itemData = new List<NewItemData>();
    #endregion

    #region Property
    public List<NewItemData> itemData => _itemData;
    public string ItemDetails(){
        string details = "";
        _itemData.ForEach( ( data ) => details += data.ToString()+"\n\n" );
        return details;
    }
    #endregion

}
