using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MallManager : MonoBehaviour/* Singleton<MallManager> */
{
    #region Field
    [SerializeField] private MallDetail                   _mallDetail;
    [SerializeField] private Transform                    _shelf;
    [SerializeField] private ProductSlot                  _slotPrefab;
    [SerializeField] private Dictionary<int, ProductSlot> _products;
    // [SerializeField] private List<ItemData>               _goods;
    [SerializeField] private List<NewItemData>               _defaultGoods;
    #endregion

    #region Property
    public MallDetail mallDetail => _mallDetail;
    #endregion

    // protected override void Awake(){
    //     base.Awake();
    //     _products = new Dictionary<int, ProductSlot>();
    //     LoadDefaultGoods();
    //     gameObject.SetActive( false );
    // }
    private void Awake(){
        _products = new Dictionary<int, ProductSlot>();
        LoadDefaultGoods();
        gameObject.SetActive( false );
    }

    #region Load Default Goods
    private void LoadDefaultGoods(){
        foreach( var goods in _defaultGoods ){
            Add( goods );
        }
    }
    #endregion

    #region Add Goods
    public bool Add( NewItemData itemData ){
        if( _products.ContainsKey( itemData.id ) ){
            Debug.Log( $"Goods {itemData.dataName} is exist." );
            return false;
        }else{
            ProductSlot slot = Instantiate( _slotPrefab, _shelf.GetComponent<ScrollRect>().content );
            slot.GenerateProduct( itemData );
            _products.Add( itemData.id, slot );
            return true;
        }
    }
    #endregion

    #region Update Shelf
    private void UpdateShelf(){
        foreach( var slot in _products ){
            slot.Value.transform.parent = _shelf;
        }
    }
    #endregion

}
