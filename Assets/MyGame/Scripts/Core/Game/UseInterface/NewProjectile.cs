using UnityEngine;
using UnityEngine.Events;

public class NewProjectile : MonoBehaviour
{
    #region Field
    [SerializeField] private int        _damage;
    [SerializeField] private float      _moveSpeed;
    [SerializeField] private int        _aoeCapacity;
    [SerializeField] private float      _aoeRadius;
    [SerializeField] private float      _maxDistance;
    [SerializeField] private LayerMask  _enemyMask;
    [SerializeField] private Transform  _target;

    private Vector2 prevPosition;
    #endregion

    #region Property
    public int   damage      => _damage;
    public float moveSpeed   => _moveSpeed;
    public float maxDistance => _maxDistance;
    public int   aoeCapacity => _aoeCapacity;
    public float aoeRadius   => _aoeRadius;
    #endregion

    #region Unity Event
    public event UnityAction OnExplosion;
    #endregion

    private void Awake()       => Initialize();
    private void FixedUpdate() => CollidedEnemy();

    #region Collision Enemy with Raycast
    private void CollidedEnemy(){
        Vector2      displacement = (Vector2)transform.position-prevPosition;
        RaycastHit2D hit2D        = Physics2D.Raycast( prevPosition, displacement.normalized, displacement.magnitude, _enemyMask );
        if( hit2D.collider != null ){
            NewEnemy enemy = hit2D.collider.GetComponent<NewEnemy>();
            enemy.DealDamage( damage );
            
            OnExplosion?.Invoke();
            if( aoeCapacity == 1 ){ return ; }

            Collider2D[] cols         = new Collider2D[aoeCapacity];
            Vector2      leftPosition = (Vector2)hit2D.collider.transform.position-new Vector2( hit2D.collider.bounds.size.x/2, 0 );
            int          count        = Physics2D.OverlapCircleNonAlloc( leftPosition, aoeRadius, cols, _enemyMask );
            for( int idx = 0; idx < count; idx++ ){
                NewEnemy aoeEnemy = cols[idx].GetComponent<NewEnemy>();
                if( enemy.Equals( aoeEnemy ) ){ continue; }

                aoeEnemy.DealDamage( damage );
            }
        }
        
        prevPosition = transform.position;
    }
    #endregion
    
    #region Weapon Data assign to projectile
    public void AssignData( WeaponData data ){
        _damage      = data.Damage;
        _moveSpeed   = data.MoveSpeed;
        _maxDistance = data.AliveDistance;
        _aoeCapacity = data.AoeCapacity;
        _aoeRadius   = data.AoeRadius;
    }
    #endregion

    #region Set Target
    public void SetTarget( Transform target ){
        _target = target;
    }
    #endregion

    #region Initialize
    private void Initialize(){
        prevPosition = transform.position;
        _enemyMask   = LayerMask.GetMask( "Enemy" );
        if( GetComponent<FollowTarget>().useLookAt ){
            GetComponent<FollowTarget>().Initialize( _target, moveSpeed, _target );
        }else{
            GetComponent<FollowTarget>().Initialize( _target, moveSpeed );
        }
        GetComponent<DestroyByDistance>().Initialize( _maxDistance, transform.position );
        OnExplosion += GetComponent<SelfDestroy>().Destroy;
    }
    #endregion

    #region Gizmos
    private void OnDrawGizmos(){
        // Gizmos.DrawSphere( transform.position, aoeRadius );
    }
    #endregion

}
