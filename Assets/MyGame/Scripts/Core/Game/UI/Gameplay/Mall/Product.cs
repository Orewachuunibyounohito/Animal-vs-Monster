using UnityEngine;

public class Product : MonoBehaviour
{
    #region Field
    // [SerializeField] private ItemData   _itemData;
    [SerializeField] private NewItemData   _itemData;
    [SerializeField] private bool       _owned;
    #endregion

    #region Property
    public  string                itemName        => _itemData.dataName;
    public  string                itemDescription => _itemData.description;
    public  int                   itemCost        => _itemData.value;
    public  ItemType              itemType        => _itemData.itemType;
    public  bool                  itemOwned       => _owned;
    private UnityEngine.UI.Button itemButton      => GetComponent<UnityEngine.UI.Button>();
    private MallDetail            mallDetail      => GameManager.Instance.MallManager.mallDetail;
    #endregion

    #region Initialize
    public void InitializeItem( NewItemData itemData ){
        _itemData = itemData;
        GetComponent<UnityEngine.UI.Image>().sprite = _itemData.image;
        itemButton.onClick.AddListener( delegate{ DetailUI( mallDetail ); } );
    }
    #endregion

    #region Show Detail & Buy UI
    public void DetailUI( MallDetail detail ){
        detail.SetDetail( this );
        
        RectTransform slotTrans = GetComponent<RectTransform>().parent.parent.GetComponent<RectTransform>();
        detail.GetComponent<RectTransform>().anchoredPosition = (Vector2)slotTrans.localPosition+slotTrans.rect.size/2;
        
        detail.gameObject.SetActive( true );
    }
    #endregion

    #region Buy the Product
    public void BuyItem() => _owned = true;
    #endregion

}
