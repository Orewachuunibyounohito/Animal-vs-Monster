using UnityEngine;

public class ProductSlot : MonoBehaviour
{
    #region Field
    [SerializeField] private GameObject productPrefab;
    #endregion

    #region Generate Product
    // public void GenerateProduct( ItemData itemData ){
    //     UnityEngine.UI.Mask mask = GetComponentInChildren<UnityEngine.UI.Mask>();
    //     Product product = Instantiate( productPrefab, mask.transform );
    //     product.InitializeItem( itemData );
    // }
    public void GenerateProduct( NewItemData itemData ){
        UnityEngine.UI.Mask mask = GetComponentInChildren<UnityEngine.UI.Mask>();
        Product product = Instantiate( productPrefab, mask.transform ).GetComponent<Product>();
        product.InitializeItem( itemData );
    }
    #endregion
}
