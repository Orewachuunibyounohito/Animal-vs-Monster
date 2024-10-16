using UnityEngine;
using UnityEngine.UI;

public class TowerSlot : MonoBehaviour
{
    #region Field
    // [SerializeField] private TowerItem _towerItemPrefab;
    [SerializeField] private GameObject _towerItemPrefab;
    #endregion

    #region Property
    private Transform _itemContent => GetComponentInChildren<Mask>().transform; 
    #endregion

    #region Generate TowerItem
    
    public void GenerateTowerItem( NewTowerData towerData ){
        TowerItem towerItem = Instantiate( _towerItemPrefab, _itemContent ).GetComponent<TowerItem>();
        towerItem.InitializeItem( towerData );
    }
    
    #endregion
}
