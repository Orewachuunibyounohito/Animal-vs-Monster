using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class TowerItem : SelectableComponenet, IBeginDragHandler, IDragHandler, IEndDragHandler, IInfoData
{   
    public enum DragState{ Standby, Dragging }
    private DragState dragState = DragState.Standby;

    #region Field
    [SerializeField] private TowerData     _towerData;
    [SerializeField] private NewTowerData  _newTowerData;
    [SerializeField] private bool          isDragging;

    private TowerItem  towerItemPrefab;
    #endregion

    #region Property
    public  NewTowerData  towerData   => _newTowerData;
    public  int           cost        => _newTowerData.cost;
    public  GameObject    towerPrefab => _newTowerData.prefab;

    public string Name { get; set; }
    public string[] Detail { get; set; }
    #endregion

    #region Event
    public UnityEvent                     OnEnterBuildArea;
    public UnityEvent<Vector2, TowerItem> OnSpawnTower;
    #endregion

    #region Const
    private const float CHECK_STATE_DURATION = 0.2f;
    #endregion

    private void Awake(){
    }

    #region Drag
    public void OnBeginDrag( PointerEventData eventData ){
        ItemBeginDrag();
    }

    public void OnDrag( PointerEventData eventData ){
        ItemDragging();
    }

    public void OnEndDrag( PointerEventData eventData ){
        ItemSpawn();
    }
    #endregion

    #region private function
    public void InitializeItem( NewTowerData towerData ){
        towerItemPrefab = this;

        _newTowerData = towerData;
        GetComponent<Image>().sprite = _newTowerData.image;

        int findParen = name.IndexOf( "(" );
        if( findParen > 0 ){
            name = name.Substring( 0, findParen );
        }
        
        OnSpawnTower.AddListener( BuildManager.Instance.Spawn );
        OnEnterBuildArea.AddListener( BuildMode.Instance.Select );

        Name   = _newTowerData.dataName;
        Detail = _newTowerData.ToStringEx();
    }

    private void CreateNewItemInSlot(){
        TowerItem itemInSlot = Instantiate( towerItemPrefab, transform.parent );
        itemInSlot.InitializeItem( _newTowerData );
    }

    private void ItemBeginDrag(){
        CreateNewItemInSlot();

        GetComponent<Image>().raycastTarget = false;
        transform.SetParent( transform.root );
        dragState = DragState.Dragging;
        
        BuildMode.Instance.ShowPreview();
    }
    private void ItemDragging(){
        Vector2 positionInWorld = Camera.main.ScreenToWorldPoint( GameManager.Instance.CustomInput.Gameplay.ScreenPosition.ReadValue<Vector2>() );
        transform.position = positionInWorld;
        isDragging = true;

        OnEnterBuildArea.Invoke();
    }
    private void ItemSpawn(){
        Destroy( gameObject );
        OnSpawnTower.Invoke( GameManager.Instance.CustomInput.Gameplay.ScreenPosition.ReadValue<Vector2>(), this );
        BuildMode.Instance.DeselectTile();
        BuildMode.Instance.HidePreview();
    }
    #endregion

    #region Fix On End 
    private void CheckEnd(){
        StartCoroutine( CheckEndCoroutine() );
    }
    System.Collections.IEnumerator CheckEndCoroutine(){
        while( true ){
            yield return new WaitForSeconds( CHECK_STATE_DURATION );
            switch( dragState ){
                case DragState.Dragging:
                    if( !isDragging ){ Destroy( gameObject ); }
                    isDragging = false;
                    break;
            }
        }
    }
    #endregion

}
