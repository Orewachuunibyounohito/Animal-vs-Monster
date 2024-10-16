using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using UnityEngine.Tilemaps;

public class BuildMode : Singleton<BuildMode>
{
    #region Field
    [SerializeField] private Tilemap     _preview;
    [SerializeField] private TileBase    _emptyTile, _buildableTile, _existTile;
    [SerializeField] private Vector3Int  _selectedTile;
    [SerializeField] private bool        _selected = false;

    public UnityEvent OnSelect;
    #endregion

    #region Property
    public Vector3Int selectedTile => _selectedTile;
    public Vector3    centerOffset => new Vector3( 0.5f, 0.5f, 0 );
    public Vector2    mouseInWorld => Camera.main.ScreenToWorldPoint( GameManager.Instance.CustomInput.Gameplay.ScreenPosition.ReadValue<Vector2>() );
    #endregion

    #region Unity Events
    protected override void Awake(){
        base.Awake();
    }
    private void Start(){}
    #endregion

    #region Integration Select
    public void Select(){
        if( _selected ){ DeselectTile(); }
        if( OutOfArea() ){
            _selected = false;
            return ;
        }

        if( TowerExist() ){
            SelectExist();
        }else{
            SelectBuildable();
        }
        _selected = true;
    }
    #endregion

    #region Check Area State
    private bool OutOfArea(){
        RaycastHit2D hit2D = Physics2D.Raycast( mouseInWorld, Vector2.zero, 20f, LayerMask.GetMask( "Tower", "TowerArea" ) );
        if( hit2D.collider == null ){ return true; }
        return false;
    }

    private bool TowerExist(){
        RaycastHit2D hit2D = Physics2D.Raycast( mouseInWorld, Vector2.zero, 20f, LayerMask.GetMask( "Tower" ) );
        if( hit2D.collider == null ){ return false; }
        return true;
    }
    #endregion

    #region Change Tile
    public void SelectBuildable(){
        _selectedTile = Vector3Int.RoundToInt( mouseInWorld-(Vector2)centerOffset );
        _preview.SetTile( _selectedTile, _buildableTile );
    }
    public void SelectExist(){
        _selectedTile = Vector3Int.RoundToInt( mouseInWorld-(Vector2)centerOffset );
        _preview.SetTile( _selectedTile, _existTile );
    }
    public void DeselectTile(){
        _selected = false;
        _preview.SetTile( _selectedTile, _emptyTile );
    }
    #endregion

    #region When Scenc Changed
    public void WhenSceneChanged( GameManager.GameState gameState ) {
        if( gameState == GameManager.GameState.Prepare ){
            _preview = GameObject.Find( "Tilemaps" ).transform.Find( "Preview" ).GetComponent<Tilemap>();
        }else{
            _preview = default;
        }
    }
    #endregion

    #region Show/Hide Preview
    public void ShowPreview() => _preview.gameObject.SetActive( true );
    public void HidePreview() => _preview.gameObject.SetActive( false );
    #endregion

    #region Test Function
    private void MouseInput(){
        if( Mouse.current.leftButton.wasPressedThisFrame ){
            DeselectTile();
            // SelectBuildable( mouseInWorld );
            SelectBuildable();
        }else if( Mouse.current.rightButton.wasPressedThisFrame ){
            DeselectTile();
        }else if( Mouse.current.middleButton.wasPressedThisFrame ){
            DeselectTile();
            // SelectExist( mouseInWorld );
            SelectExist();
        }
    }
    #endregion
    
}
