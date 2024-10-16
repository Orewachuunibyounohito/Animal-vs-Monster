using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Enemy : Damageable
{   
    #region Field
    [SerializeField] private EnemyData _enemyData;
    #endregion

    #region Property
    public int        defense  => _enemyData.defense;
    public ulong      strength => _enemyData.strength;
    public float      speed    => _enemyData.speed;
    public RewardData reward   => _enemyData.reward;

    public EnemyType type => _enemyData.enemyType;
    #endregion

    #region Event
    public UnityEvent OnDie;
    #endregion

    protected override void Awake(){
        _maxHp = _enemyData.maxHp;
        // GetComponent<MoveForward>().SetVelocity( _enemyData.speed*new Vector2( -1, 0 ) );
        base.Awake();
    }

    public override void DealDamage( int amount )
    {
        int finalDamage = System.Math.Clamp( amount-defense, 0, int.MaxValue );
        base.DealDamage( finalDamage );
        CheckDie();
    }

    #region Check Die
    public void CheckDie(){
        if( _hp != 0 ){ return ; }
        // gameObject.layer = LayerMask.GetMask( "Nothing" );
        OnDie.Invoke();
    }
    #endregion

    #region Initialize
    public void Initialize( List<Vector2> path ){
        GetComponent<MoveWithPath>().path = path;
        GetComponent<MoveWithPath>().speed = speed;
        // GetComponent<EnterHurtArea>().OnEnter.AddListener( delegate{ GameManager.Instance.player.EnemyEnterWithType( type ); } );
        GetComponent<EnterHurtArea>().OnEnter.AddListener( GameManager.Instance.EnemyDie );
        // OnDie.AddListener( delegate{ GameManager.Instance.player.Loot( this ); } );
        OnDie.AddListener( GameManager.Instance.EnemyDie );
    }
    #endregion
    
    #region Help Function
    [ContextMenu( "Calculate Drop Rate" )]
    public void DropRate(){
        foreach( var item in reward.itemData ){
            float dropThreshold = Random.Range( 0f, 1f );
            float dropRate      = strength/(float)item.dropRateRare;
            int   dropCount     = (int)( strength/(float)item.dropCountRare*Random.Range( 1-item.DropRange, 1f ) );
            if( dropCount < item.CountLeast ){
                dropCount = item.CountLeast;
            }
            Debug.Log( $"{item.itemName}:\n - Drop Rate = {dropRate*100}%\n - Drop Count = {dropCount}" );
            Debug.Log( $"This threshold: {dropThreshold}\nDrop: { dropThreshold < dropRate}" );
        }
    }
    #endregion
    
}
