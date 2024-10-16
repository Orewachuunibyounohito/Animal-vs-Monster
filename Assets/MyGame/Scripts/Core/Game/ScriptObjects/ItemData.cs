using UnityEngine;

[System.Serializable]
[CreateAssetMenu( fileName = "NewItem", menuName = "TD/Item Data" )]
public class ItemData : ScriptableObject
{
    #region Field
    [SerializeField] private int      _id;
    [SerializeField] private string   _itemName;
    [SerializeField] private int      _value;
    [SerializeField] private ulong    _dropRateRare;
    [SerializeField] private ulong    _dropCountRare;
    [SerializeField] private bool     _stackable;
    [SerializeField] private string   _description;
    [SerializeField] private Sprite   _image;
    [SerializeField] private ItemType _itemType;

    private const int   DROP_COUNT_AT_LEAST = 1;
    private const float DROP_COUNT_RANGE    = 0.3f; // dropCount - 0.3 => 0.7 ~ 1.0
    #endregion

    #region Property
    public int      id            => _id;
    public string   itemName      => _itemName;
    public int      value         => _value;
    public ulong    dropRateRare  => _dropRateRare;
    public ulong    dropCountRare => _dropCountRare;
    public bool     stackable     => _stackable;
    public string   description   => _description;
    public Sprite   image         => _image;
    public ItemType itemType      => _itemType;
    public int      CountLeast    => DROP_COUNT_AT_LEAST;
    public float    DropRange     => DROP_COUNT_RANGE;
    #endregion

    #region ToString
    public override string ToString()
    {
        // return $"No.{id}:[ name={itemName}, value={value}, dropRateRare={dropRateRare}, dropCountRare={dropCountRare}, stackable={stackable}, description={description}, itemType={itemType} ]";
        return $"No. {id}:\nname: {itemName}\nvalue: {value}\ndropRateRare: {dropRateRare}\ndropCountRare: {dropCountRare}\nstackable: {stackable}\ndescription: {description}\nitemType: {itemType}";
    }
    #endregion
}
