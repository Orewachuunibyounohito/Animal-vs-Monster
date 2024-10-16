using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent( typeof( Rigidbody2D ) )]
public class MoveWithPath : MonoBehaviour
{
    #region Field
    [SerializeField] private List<Vector2> _path;
    [SerializeField] private bool          _loop;
    [SerializeField] private int           _currentDestination = -1;
    [SerializeField] private float         _speed;
    
    [Tooltip("-- Debug --"), ReadOnly]
    [SerializeField] private Vector2       _velocity;
    #endregion

    #region Property
    public  Rigidbody2D rb           => GetComponent<Rigidbody2D>();
    private Vector2     nextPosition => _path[_currentDestination];

    public List<Vector2> path{
        get{ return _path; }
        set{ _path = value; }
    }
    public bool loop{
        get{ return _loop; }
        set{ _loop = value; }
    }
    public float speed{
        get{ return _speed; }
        set{ _speed = value; }
    }
    #endregion

    #region Const
    private const float DIRECTION_TOLERANCE = 0.05f;
    #endregion

    #region Unity Event
    public UnityAction<float> OnDirectionChanged;
    #endregion
    
    void OnEnable() => GameManager.Instance.GameOver += OnGameOver;

    private void Start(){
        if( _path.Count == 0 ){
            // SetPathFromSpawnEnemy();
            Debug.Log( $"{name} is not assigned move path, please check SpawnEnemy" );
        }
        StartMove();
    }

    private void FixedUpdate(){
        Moving();
    }

    void OnDisable() => GameManager.Instance.GameOver -= OnGameOver;

    #region Move Function
    public void StartMove(){
        _currentDestination = -1;
        ToNextPoint();
    }
    private void Moving(){
        float distance = Vector2.Distance( rb.position, nextPosition );
        if( distance < rb.velocity.magnitude*Time.fixedDeltaTime || Mathf.Approximately( distance, 0 ) ){
            ToNextPoint();
        }
    }

    private void ToNextPoint(){
        _currentDestination++;
        rb.velocity = Vector2.zero;
        
        float   impulse   = _speed*Time.fixedDeltaTime*20f*rb.mass;
        Vector2 direction = ( _path[_currentDestination]-rb.position ).normalized;
        rb.AddForce( impulse*direction, ForceMode2D.Impulse );
        if( Mathf.Abs( rb.velocity.x-Mathf.RoundToInt( rb.velocity.x ) ) > DIRECTION_TOLERANCE ){
            OnDirectionChanged.Invoke( rb.velocity.x );
        }
        _velocity = rb.velocity;
    }
    #endregion

    private void OnGameOver() => rb.velocity = Vector2.zero;

    #region Test Function
    // private void SetPathFromSpawnEnemy(){
    //     SpawnEnemy.Instance.AssignPath( ref _path );
    // }
    #endregion
}
