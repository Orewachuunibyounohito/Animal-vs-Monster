using System;
using System.Collections;
using TD.Info;
using UnityEngine;

public class NewAttacker : MonoBehaviour, IAttacker
{
    #region Enum
    public enum State{ Detecting, Lock, Attacking }
    protected State state = State.Detecting;
    #endregion

    #region Field
    [SerializeField] protected float     _attackRange;
    [SerializeField] protected float     _attackSpeed;
    [SerializeField] protected bool      _inCoolDown = true;
    [SerializeField] protected NewWeapon _weapon;
    [SerializeField] protected LayerMask _enemyMask;
    [SerializeField] protected string    _behaviourState = "Idle";
    [SerializeField] protected string    _selectState = "";

    [SerializeField] protected NewDeathable _target;
    private bool isGameOver = false;
    #endregion

    #region Property
    protected virtual Animator  animator   => GetComponent<Animator>();
    protected virtual bool      OutOfRange => Vector2.Distance( _target.transform.position, transform.position ) >= _attackRange? true : false;
    
    public bool IsIdle => _behaviourState == "Idle";
    #endregion

    #region Const
    protected const string ENEMY_LAYER_NAME = "Enemy";
    #endregion

    protected virtual void Awake(){
        int parenIndex = name.IndexOf( '(' );
        name = parenIndex == -1? name : name.Remove( parenIndex );
        _enemyMask = LayerMask.GetMask( ENEMY_LAYER_NAME );
        _weapon    = GetComponent<NewWeapon>();
        _weapon.SetWeaponData( GameManager.Instance.Library.GetData<NewTowerData>(name).weaponData );
        _weapon.OnAttack.AddListener( AttackCoolDown );
        _weapon.OnAttack.AddListener(AttackAudio);
        
        _attackRange = _weapon.attackRange;
        _attackSpeed = _weapon.attackSpeed;

        var attackArea = Instantiate(PrefabRepository.AttackArea, transform);
        attackArea.name = "AttackArea";
        attackArea.GetComponent<SpriteRenderer>().size = Vector2.one * (_attackRange * 2);
        attackArea.SetActive(false);

        GameManager.Instance.GameOver += OnGameOver;
    }

    protected virtual void Start(){
        AttackCoolDown();
    }

    protected virtual void FixedUpdate(){
        if(isGameOver) return ;
        switch( state ){
            case State.Detecting:
                Detecting();
                break;
            case State.Lock:
                try{
                    SimpleLookAt();
                    if( OutOfRange )      { TargetLost(); }
                    else if( _inCoolDown ){ return ; }
                    else                  { Attack( _target ); }
                }catch( Exception ex ){
                    TargetLost();
                    Debug.Log( $"{ex}" );
                }
                break;
        }
    }

    #region Detecting Target
    protected virtual void Detecting(){
        Collider2D hit2D = Physics2D.OverlapCircle( transform.position, _attackRange, _enemyMask );
        if( hit2D != null ){
            if( hit2D.TryGetComponent( out _target ) ){
                _target.CreatureDied.AddListener( TargetLost );
                state   = State.Lock;
            }
        }
    }
    protected virtual void TargetLost(){
        _target.CreatureDied.RemoveListener( TargetLost );
        _target = default;
        state   = State.Detecting;
    }
    #endregion

    #region Simple Look At
    protected virtual void SimpleLookAt(){
        if( transform.position.x <= _target.transform.position.x ){ transform.rotation = Quaternion.Euler( 0f, 0f, 0f ); }
        else{ transform.rotation = Quaternion.Euler( 0f, 180f, 0f ); }
        
        Debug.DrawLine(transform.position, _target.transform.position);
    }
    #endregion

    #region Attack Cool Down
    public void AttackCoolDown(){
        StartCoroutine( CoolDownCoroutine() );
    }
    protected virtual IEnumerator CoolDownCoroutine(){
        yield return new WaitForSeconds( 1f/_attackSpeed );
        _inCoolDown = false;
    }
    #endregion

    #region Change Animation State
        public void ToSelected(){
            if(_selectState.Equals("_Selected")){ return ; }
            _selectState = "_Selected";
            animator.Play( _behaviourState + _selectState, 0, animator.GetCurrentAnimatorStateInfo( 0 ).normalizedTime );
        }
        public void ToDeselected(){
            if(_selectState.Equals("")){ return ; }
            _selectState = "";
            animator.Play( _behaviourState + _selectState, 0, animator.GetCurrentAnimatorStateInfo( 0 ).normalizedTime );
        }
        public void ToAttack(){
            _behaviourState = "Attack";
            animator.Play(_behaviourState + _selectState, 0, 0);
        }
        public void ToIdle(){
            _behaviourState = "Idle";
            animator.Play(_behaviourState + _selectState, 0, 0);
        }
    #endregion

    #region Gizmos
    protected virtual void OnDrawGizmos(){
        // Gizmos.DrawRay( transform.position, Vector3.right*_attackRange );
        // Gizmos.DrawLine( transform.position, (Vector2)transform.position+Vector2.right*_attackRange );
        // Gizmos.DrawSphere( transform.position, _attackRange );
    } 
    #endregion

    #region IAttacker
    public void Attack( IDamageable target ){
        _inCoolDown = true;
        var targetTrans = ((MonoBehaviour)target).transform;
        _weapon.DoAttack( targetTrans );
    }
    #endregion

    private void AttackAudio(){
        GameManager.Instance.AudioPlayer.PlaySfx(_weapon.SfxName);
    }

    private void OnGameOver(){
        isGameOver = true;
        if(_target){ TargetLost(); }
    }
}
