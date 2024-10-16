using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attacker : MonoBehaviour
{
    #region Enum
    public enum State{ Detecting, Lock, Attacking }
    State state = State.Detecting;
    #endregion

    #region Field
    [SerializeField] private float     _attackRange;
    [SerializeField] private float     _attackSpeed;
    [SerializeField] private bool      _inCoolDown = true;
    [SerializeField] private Weapon    _weapon;
    [SerializeField] private LayerMask _enemyMask;
    [SerializeField] private string    _animationState = "Idle";
    [SerializeField] private List<Vector2[]> _attackPaths = new List<Vector2[]>();

    [SerializeField] private Transform _target;
    #endregion

    #region Property
    private Animator  animator   => GetComponent<Animator>();
    private bool      OutOfRange => Vector2.Distance( _target.position, transform.position ) >= _attackRange? true : false;
    #endregion

    private void Awake(){
        _enemyMask = LayerMask.GetMask( "Enemy" );
        _weapon.OnAttack.AddListener( AttackCoolDown );
        if( _weapon.type == WeaponType.Melee ){
            AttackPathAssign();
        }
    }

    private void Start(){
        // Dictionary<string, TowerData> towers = GameManager.Instance.towerLibrary.TowerDictionary;
        // if( towers.TryGetValue( name, out TowerData data ) ){
        //     _attackRange = data.weaponData.AliveDistance;
        //     _attackSpeed = data.weaponData.AttackSpeed;
        // }
        var tower = GameManager.Instance.Library.GetData<NewTowerData>(name);
        _attackRange = tower.weaponData.AliveDistance;
        _attackSpeed = tower.weaponData.AttackSpeed;
        AttackCoolDown();
    }

    private void FixedUpdate(){
        switch( state ){
            case State.Detecting:
                Detecting();
                break;
            case State.Lock:
                try{
                    SimpleLookAt();
                    if( OutOfRange ){ TargetLost(); }
                    else            { Attack(); }
                }catch( Exception ex ){
                    TargetLost();
                    Debug.Log( $"{ex}" );
                }
                break;
        }
    }

    #region Detecting Target
    private void Detecting(){
        Collider2D hit2D = Physics2D.OverlapCircle( transform.position, _attackRange, _enemyMask );
        if( hit2D != null ){
            _target = hit2D.transform;
            state   = State.Lock;
        }else{
            ReturnDefaultRotation();
        }
    }
    private void TargetLost(){
        _target = default;
        state   = State.Detecting;
    }
    #endregion

    #region Simple Look At
    private void SimpleLookAt(){
        switch( _weapon.type ){
            case WeaponType.Melee:
                if( _inCoolDown ){ return ; }
                transform.rotation = Quaternion.AngleAxis( Vector2.SignedAngle( Vector2.right, _target.position-transform.position ), Vector3.forward );
                break;
            case WeaponType.Ranged:
                if( transform.position.x <= _target.position.x ){ transform.rotation = Quaternion.Euler( 0f, 0f, 0f ); }
                else{ transform.rotation = Quaternion.Euler( 0f, 180f, 0f ); }
                break;
        }

    }
    private void ReturnDefaultRotation(){
        if( _inCoolDown ){ return ; }
        switch( _weapon.type ){
            case WeaponType.Melee:
                if( transform.rotation.x <= 90 && transform.rotation.x >= -90 ){ transform.rotation = Quaternion.Euler( 0f, 0f, 0f ); }
                else{ transform.rotation = Quaternion.Euler( 0f, 180f, 0f ); }
                break;
        }
    }
    #endregion

    #region Attack Target
    private void Attack(){
        if( _inCoolDown ){ return ; }
        _inCoolDown = true;
        _weapon.DoAttack( _target );
    }
    #endregion

    #region Attack Cool Down
    public void AttackCoolDown(){
        StartCoroutine( CoolDownCoroutine() );
    }
    private IEnumerator CoolDownCoroutine(){
        yield return new WaitForSeconds( 1f/_attackSpeed );
        _inCoolDown = false;
    }
    #endregion

    #region Melee Attack

        #region Change State
        public void AnimationDo( bool isContinue=false ){
            if( isContinue ){ 
                animator.Play( _animationState, 0, animator.GetCurrentAnimatorStateInfo( 0 ).normalizedTime );
            }else{
                animator.Play( _animationState );
            }
        }
        public void ChangeAnimationState( bool Select ){
            if( Select ){
                ChangeAnimationState( _animationState+"_Selected" );
            }else{
                ChangeAnimationState( _animationState.Substring( 0, _animationState.IndexOf( '_' ) ) );
            }
        }
        public void ChangeAnimationState( string state ){
            _animationState = state;
        }
        public bool IsSelectedState(){
            return _animationState.IndexOf( '_' ) >= 0? true : false;
        }
        public void ToAttackState(){
            state = State.Attacking;
        }
        public void AttackExit(){
            state = State.Lock;
        }
        #endregion

        #region Attack Path Assign
        private void AttackPathAssign(){
            PolygonCollider2D polygon = transform.GetChild(0).GetComponent<PolygonCollider2D>();
            for( int idx = 0; idx < polygon.pathCount; idx++ ){
                _attackPaths.Add( polygon.GetPath( idx ) );
            }
            GetComponent<PolygonCollider2D>().SetPath( 0, _attackPaths[0] );
        }
        #endregion

        #region Attack Path Changed
        public void AttackPathChanged( int pathIndex ){
            GetComponent<PolygonCollider2D>().SetPath( 0, _attackPaths[pathIndex] );
        }
        #endregion

        #region Attack Trigger
        private void OnTriggerEnter2D( Collider2D other ){
            if( _weapon.type == WeaponType.Ranged ){ return ; }
            if( GetComponent<PolygonCollider2D>().enabled ){
                // Debug.Log( "In HitBox" );
                if( other.CompareTag( "Enemy" ) ){
                    other.GetComponent<Enemy>().DealDamage( _weapon.damage );
                    other.GetComponent<Enemy>().UpdateUI();
                }
                // if( other.CompareTag( "Enemy" ) ){
                //     other.GetComponent<NewEnemy>().DealDamage( _weapon.damage );
                // }
            }
        }
        #endregion

    #endregion

    #region Gizmos
    private void OnDrawGizmos(){
        // Gizmos.DrawRay( transform.position, Vector3.right*_attackRange );
        // Gizmos.DrawLine( transform.position, (Vector2)transform.position+Vector2.right*_attackRange );
        // Gizmos.DrawSphere( transform.position, _attackRange );
    } 
    #endregion

}
