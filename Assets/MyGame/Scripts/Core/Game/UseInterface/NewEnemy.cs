using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

[RequireComponent( typeof( MoveWithPath ), typeof( EnterHurtArea ) )]
public class NewEnemy : NewDeathable
{   
    #region Field
    [SerializeField] private NewEnemyData _enemyData;
    #endregion

    #region Property
    public int        defense      => _enemyData.defense;
    public ulong      strength     => _enemyData.strength;
    public RewardData reward       => _enemyData.reward;
    public float      heathPercent => _currentHeath/(float)_maxHealth;

    public EnemyType type => _enemyData.enemyType;

    public List<Vector2> path{
        get => GetComponent<MoveWithPath>().path;
        set => GetComponent<MoveWithPath>().path = value;
    }
    public float speed{
        get => GetComponent<MoveWithPath>().speed;
        set => GetComponent<MoveWithPath>().speed = value;
    }
    #endregion

    #region Event
    public UnityEvent  EnemyEscaped{
        get => GetComponent<EnterHurtArea>().OnEnter;
        set => GetComponent<EnterHurtArea>().OnEnter = value;
    }
    public UnityAction<float> DirectionChanged{
        get => GetComponent<MoveWithPath>().OnDirectionChanged;
        set => GetComponent<MoveWithPath>().OnDirectionChanged = value;
    }
    #endregion

    protected override void Awake(){
        name = name.Remove( name.IndexOf( '(' ) );
        _enemyData = GameManager.Instance.Library.GetData<NewEnemyData>(name);
        _maxHealth = _enemyData.maxHp;
        speed      = _enemyData.speed;
        base.Awake();
    }
    private void Start(){        
        EnemyEscaped.AddListener( OnEscape );
        CreatureDied.AddListener( OnDie );
        OnTakeDamage += HurtAudio;
    }

    #region Private Methods
    private void OnEscape(){
        GameManager.Instance.newPlayer.EnemyEnterWithType( type );
        GameManager.Instance.EnemyDie();
        GameManager.Instance.InstantiateAliveEnemyText(Camera.main.transform.position);
    }
    private void OnDie(){
        GameManager.Instance.newPlayer.Loot( this );
        GameManager.Instance.EnemyDie();
        var textPos = (Vector2)transform.position + new Vector2(0, GetComponentInChildren<SpriteRenderer>().size.y / 2);
        GameManager.Instance.InstantiateAliveEnemyText(textPos);
    }
    private void HurtAudio(){
        GameManager.Instance.AudioPlayer.PlaySfx(_enemyData.SfxName);
    }
    #endregion

    #region Override Function
    public override void DealDamage( int amount )
    {
        int finalDamage = System.Math.Clamp( amount-defense, 0, int.MaxValue );
        base.DealDamage( finalDamage );
    }
    #endregion

    #region Initialize
    public void Initialize( List<Vector2> path ){
        this.path = path;
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
