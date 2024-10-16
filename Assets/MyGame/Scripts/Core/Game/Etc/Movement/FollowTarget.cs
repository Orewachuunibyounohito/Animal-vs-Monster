using System;
using UnityEngine;

public class FollowTarget : MonoBehaviour
{
    #region Field
    [SerializeField] private Transform _target;
    [SerializeField] private Transform _lookAt;
    [SerializeField] private float     _speed;
    [SerializeField] private bool      _useFollow;
    [SerializeField] private bool      _useLookAt;
    [SerializeField] private Vector2   _towardTarget;
    [SerializeField] private Vector2   _lastDisplacement;
    #endregion

    #region Property
    public  bool        useLookAt => _useLookAt;
    private Rigidbody2D rb        => GetComponent<Rigidbody2D>();
    #endregion
    
    #region Unity Events
    private void Awake(){
        _target.GetComponent<NewEnemy>().CreatureDied.AddListener( TargetDied );
    }
    private void FixedUpdate(){
        Follow();
        LookAt();
    }
    #endregion

    #region Follow & LookAt
    private void Follow(){
        float step = _speed*Time.fixedDeltaTime;
        if( _useFollow ){
            try{
                if( _target == default || _target == null ){
                    // Debug.Log( $"No target, check {this} script's Follow Function." );
                    rb.MovePosition( (Vector2)transform.position+_lastDisplacement );
                    return ;
                }
                Vector2 newPosition = Vector2.MoveTowards( transform.position, _target.position, step );
                _lastDisplacement   = Vector2.Distance( newPosition, transform.position ) < step*0.9f? _lastDisplacement : newPosition-(Vector2)transform.position;
                rb.MovePosition( newPosition );
            }catch( Exception ex ){
                _target = default;
                Debug.Log( $"FollowTarget Error:\n{ex}" );
            }
        }else{
            rb.MovePosition( (Vector2)transform.position+_towardTarget*step );
        }
    }
    private void LookAt(){
        if( _lookAt == default ){
            // Debug.Log( $"No use LookAt." );
            return ;
        }
        // rb.transform.LookAt( _lookAt, Vector3.up );
        Vector2 direction = ( (Vector2)_lookAt.position-rb.position ).normalized;
        rb.transform.rotation = Quaternion.AngleAxis( Vector2.SignedAngle( Vector2.right, direction ), Vector3.forward );
    }
    #endregion

    #region Public Methods
    public void Initialize( Transform target, float speed, Transform lookAt=default ){
        SetTarget( target );
        SetLookAt( lookAt );
        SetSpeed( speed );
    }  
    public void SetTarget( Transform target ){
        _target = target;
        _towardTarget = ( _target.position-transform.position ).normalized;
    }
    public void SetLookAt( Transform target ){
        _lookAt = target;
    }
    public void SetSpeed( float speed ){
        _speed = speed;
    }
    public void TargetDied(){
        _target = default;
    }
    #endregion
}
