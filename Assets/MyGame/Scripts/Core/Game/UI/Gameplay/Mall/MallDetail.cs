using UnityEngine;
using UnityEngine.InputSystem;

public class MallDetail : MonoBehaviour
{
    #region Field
    [SerializeField] private UnityEngine.UI.Button _buyButton;
    [SerializeField] private TMPro.TextMeshProUGUI _itemTitle;
    [SerializeField] private TMPro.TextMeshProUGUI _itemDescription;
    [SerializeField] private TMPro.TextMeshProUGUI _itemCost;
    [SerializeField] private LayerMask             _uiMask;

    private InputActions.GameplayActions gameplayActions;
    #endregion
    
    #region Property
    // private Player player    =>  .player;
    private NewPlayer player    => GameManager.Instance.newPlayer;
    public  int       lineCount => _itemDescription.textInfo.lineCount;
    #endregion

    private void Awake(){
        _itemDescription.OnPreRenderText += delegate{
            float spacing   = _itemDescription.fontSize/6;
            float fontSize  = _itemDescription.fontSize;
            float newHeight = lineCount*( fontSize+spacing )-spacing;
            float padding   = fontSize;
            _itemDescription.rectTransform
                           .SetSizeWithCurrentAnchors( RectTransform.Axis.Vertical, newHeight );
            RectTransform content = _itemDescription.transform.parent.GetComponent<RectTransform>();
            content.SetSizeWithCurrentAnchors( RectTransform.Axis.Vertical, newHeight+padding );
        };
        gameplayActions = GameManager.Instance.CustomInput.Gameplay;
        gameplayActions.Interact.performed         += HideDetail;
        gameplayActions.InteractForTouch.performed += HideDetail;
    }

    #region Detail Assign
    public void SetDetail( Product product ){
        _itemTitle.SetText( product.itemName );
        _itemDescription.SetText( product.itemDescription );
        _itemCost.SetText( $"$ {product.itemCost}" );

        _buyButton.interactable = !product.itemOwned;
        _buyButton.onClick.RemoveAllListeners();
        _buyButton.onClick.AddListener( delegate{ SellItem( product ); } );
    }
    #endregion

    #region Mall Sell Item
    public void SellItem( Product product ){
        if( player.Money < product.itemCost ){
            Debug.Log( $"Not enough money, can't buy {product.itemName}" );
            return ;
        }

        switch( product.itemType ){
            case ItemType.Tower:
                player.ActivateTower(product.itemName);
                player.Consume( product.itemCost );
                product.BuyItem();
                _buyButton.interactable = false;
                break;
        }
    }
    #endregion

    #region Hide Detail
    private void HideDetail(InputAction.CallbackContext callback){
        if(callback.performed){
            Ray          ray   = Camera.main.ScreenPointToRay( GameManager.Instance.CustomInput.Gameplay.ScreenPosition.ReadValue<Vector2>() );
            RaycastHit2D hit2D = Physics2D.GetRayIntersection( ray, float.MaxValue, _uiMask );
            if(hit2D.collider == null){ return ; }
            if( hit2D.collider.tag != "MallDetail" ){ gameObject.SetActive( false ); }
        }
    }
    #endregion
    
}
