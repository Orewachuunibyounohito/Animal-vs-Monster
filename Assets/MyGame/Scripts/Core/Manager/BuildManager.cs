using TD.Info;
using UnityEngine;
using UnityEngine.Events;

public class BuildManager : Singleton<BuildManager>
{
    #region Field
    [SerializeField] private LayerMask    _layerMask, _areaMask;
    [SerializeField] private Vector2      _pivotCenter;
    [SerializeField] private Transform    _towerCollection;
    #endregion

    #region Event
    public UnityEvent<int> OnBuy;
    #endregion

    #region Const
    private const float CURSOR_RADIUS = 0.3f;
    #endregion

    protected override void Awake(){
        base.Awake();
        _pivotCenter = new Vector2( 0.5f, -0.5f );
    }

    #region Integration
    public void Spawn( Vector2 position, TowerItem tower ){
        Vector2 unitUpperLeft = MapToWorldUnit( position );
        
        if( !InTowerArea( position ) ){ return ; }
        if( HasTowerInThisUnit( position ) ){ return; }

        SpawnTowerWithUnit( unitUpperLeft+_pivotCenter, tower );
    }
    #endregion
    
    #region Get mouse position & convert to unit
    private Vector2 MapToWorldUnit( Vector2 position ){
        // Use Camera.main.ScreenToWorldPoint to get mouse position in Game.
        Vector2 unitInWorld = Camera.main.ScreenToWorldPoint( position );

        // Unit center.upper left corner -> ( -0.5f, 0.5f, 0f )
        Vector2 upperLeft = new Vector2( -0.5f, 0.5f );

        // Add center offset & Round it to get each UpperLeft of unit
        unitInWorld  += upperLeft;
        unitInWorld.x = Mathf.Round( unitInWorld.x );
        unitInWorld.y = Mathf.Round( unitInWorld.y );

        // Debug.Log( $"Unit: {unitInWorld}" );
        return unitInWorld;
    }

    private bool HasTowerInThisUnit( Vector2 position ){
        // In this case, Ray is not a good idea, mouse near edge will not be detected.

        Vector2      unit = Camera.main.ScreenToWorldPoint( position );
        Collider2D[] cols = new Collider2D[1];
        return Physics2D.OverlapCircleNonAlloc( unit, CURSOR_RADIUS, cols, _layerMask ) > 0? true : false;
    }
    private bool InTowerArea( Vector2 position ){
        Ray          ray   = Camera.main.ScreenPointToRay( position );
        RaycastHit2D hit2D = Physics2D.Raycast( ray.GetPoint( 0f ), ray.direction, 20f, _areaMask );
        return hit2D.collider != null? true : false;
    }
    #endregion

    #region Spawn
    private void SpawnTowerWithUnit( Vector2 unitCenter, TowerItem tower ){
        if( unitCenter == default ){  return ; }
        if( !GameManager.Instance.NewMoneyEnough( tower.cost ) ){
            Debug.Log( $"Can't buy {tower.towerPrefab.name}\nMoney not enough." );
            return ;
        }

        GameObject towerObj = Instantiate( tower.towerPrefab, unitCenter, tower.towerPrefab.transform.rotation, _towerCollection );
        var selected = towerObj.AddComponent<SelectedTower>();
        selected.SetMenuPresenter(GameManager.Instance.gameplayUI.GetFunctionalPanel());
        selected.SellingTower += () => {
            Destroy(selected.gameObject);
            GameManager.Instance.newPlayer.MakeMoney(tower.cost);
        };

        OnBuy.Invoke( tower.cost );
    }
    #endregion

    #region When Enter Battle Scene
    public void WhenSceneChanged( GameManager.GameState gameState ){
        if( gameState == GameManager.GameState.Prepare ){
            _towerCollection = new GameObject( "Tower Collection" ).transform;
        }else{
            _towerCollection = default;
        }
    }
    #endregion

    #region Gizmos
    // private void OnDrawGizmos(){

    //     if( Input.GetMouseButton( 0 ) ){
    //         Gizmos.DrawSphere( Camera.main.ScreenToWorldPoint( Input.mousePosition ), CURSOR_RADIUS );
    //     }
    // }
    #endregion

}
