using Sirenix.OdinInspector;
using Unity.VisualScripting;
using UnityEngine;

[System.Serializable]
[CreateAssetMenu( fileName = "NewItem", menuName = "TD/New Item Data" )]
public class NewItemData : InstantiateData
{
    #region Field
    [LabelText("Type")]
    [VerticalGroup(GENERAL_LEFT + "/Right")]
    [BoxGroup(SPLIT_GENERAL)]
    [SerializeField] private ItemType _itemType;

    [BoxGroup(SPLIT + "/Stats")]
    [SerializeField] private ulong    _dropRateRare;

    [BoxGroup(SPLIT + "/Stats")]
    [SerializeField] private ulong    _dropAmountRare;

    [BoxGroup(SPLIT + "/Stats")]
    [SerializeField] private int      _value;

    [BoxGroup(SPLIT + "/Stats")]
    [SerializeField] private bool     _stackable;

    [BoxGroup(SPLIT + "/Stats")]
    [ShowIf("_stackable")]
    [SerializeField] private ulong    _capacity;

    [HideLabel, TextArea(4, 14)]
    [BoxGroup(SPLIT + "/Description")]
    [SerializeField] private string   _description;

    private const int   DROP_COUNT_AT_LEAST = 1;
    private const float DROP_COUNT_RANGE    = 0.3f; // dropCount - 0.3 => 0.7 ~ 1.0
    #endregion

    #region Property
    public int      value         => _value;
    public ulong    dropRateRare  => _dropRateRare;
    public ulong    dropAmountRare => _dropAmountRare;
    public bool     stackable     => _stackable;
    public string   description   => _description;
    public ulong    Capacity      => _capacity;
    public ItemType itemType      => _itemType;
    public int      CountLeast    => DROP_COUNT_AT_LEAST;
    public float    DropRange     => DROP_COUNT_RANGE;
    #endregion

    void OnValidate(){
        _dropAmountRare = _dropAmountRare <= 0? 1 : _dropAmountRare;
    }

    #region ToString
    public override string ToString()
    {
        // return $"No.{id}:[ name={itemName}, value={value}, dropRateRare={dropRateRare}, dropCountRare={dropCountRare}, stackable={stackable}, description={description}, itemType={itemType} ]";
        return $"No. {id}:\nname: {dataName}\nvalue: {value}\ndropRateRare: {dropRateRare}\ndropAmountRare: {dropAmountRare}\nstackable: {stackable}\ndescription: {description}\nitemType: {itemType}";
    }
    public string[] ToStringEx(){
        var result = new string[]{
            $"value:\n" + 
            $"dropRateRare:\n" + 
            $"dropAmountRare:\n" + 
            $"stackable:\n" + 
            $"description:\n" + 
            $"itemType:",

            $"{value}\n" + 
            $"{dropRateRare}\n" + 
            $"{dropAmountRare}\n" + 
            $"{stackable}\n" + 
            $"{description}\n" + 
            $"{itemType}"
        };
        return result;
    }
    #endregion

    #region Help Function
    public void AssignFromOldData( ItemData data ){
        // _prefab        = data.itemPrefab;
        _value         = data.value;
        _dropRateRare  = data.dropRateRare;
        _dropAmountRare = data.dropCountRare;
        _stackable     = data.stackable;
        _description   = data.description;
        switch( data.itemType ){
            case ItemType.Coin:
                _itemType = ItemType.Coin;
                break;
            case ItemType.Tower:
                _itemType = ItemType.Tower;
                break;
            case ItemType.Consumeable:
                _itemType = ItemType.Consumeable;
                break;
            case ItemType.Etc:
                _itemType = ItemType.Etc;
                break;
        }
    }
    #endregion

}
