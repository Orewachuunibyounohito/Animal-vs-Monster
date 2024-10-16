using Sirenix.OdinInspector;
using UnityEngine;

[CreateAssetMenu( fileName = "EnemyData", menuName = "TD/New Enemy Data", order = 2 )]
public class NewEnemyData : InstantiateData
{
    #region Field
    [BoxGroup(SPLIT + "/Stats")]
    [HorizontalGroup(SPLIT + "/Stats/Split", LabelWidth = 60)]
    [VerticalGroup(SPLIT + "/Stats/Split/Middle")]
    [SerializeField] private int        _maxHp;

    [VerticalGroup(SPLIT + "/Stats/Split/Middle")]
    [BoxGroup(SPLIT + "/Stats")]
    [SerializeField] private int        _defense;

    [VerticalGroup(SPLIT + "/Stats/Split/Middle")]
    [BoxGroup(SPLIT + "/Stats")]
    [SerializeField] private float      _speed;

    [VerticalGroup(SPLIT + "/Stats/Split/Middle")]
    [BoxGroup(SPLIT + "/Stats")]
    [SerializeField] private ulong      _strength;

    [Title("SFX", TitleAlignment = TitleAlignments.Centered)]
    [HideLabel, SerializeField]
    private SfxName _sfxName;

    [Title("Reward", TitleAlignment = TitleAlignments.Centered)]
    [InlineEditor]
    [HideLabel]
    [SerializeField] private RewardData _reward;

    [LabelText("Type")]
    [VerticalGroup(GENERAL_LEFT + "/Right")]
    [BoxGroup(SPLIT_GENERAL)]
    [SerializeField] private EnemyType  _enemyType;

    #endregion

    #region Property
    public int        maxHp       => _maxHp;
    public int        defense     => _defense;
    public float      speed       => _speed;
    public ulong      strength    => _strength;
    public RewardData reward      => _reward;
    public EnemyType  enemyType   => _enemyType;
    public SfxName    SfxName     => _sfxName;
    #endregion

    public string[] ToStringEx(){
        return new []{"", ""};
    }

    #region Help Function
    public void AssignFromOldData( EnemyData data ){
        _prefab   = data.enemyPrefab;
        _maxHp    = data.maxHp;
        _defense  = data.defense;
        _speed    = data.speed;
        _strength = data.strength;
        _reward   = data.reward;
        switch( data.enemyType ){
            case EnemyType.Normal:
                _enemyType = EnemyType.Normal;
                break;
            case EnemyType.Strong:
                _enemyType = EnemyType.Strong;
                break;
            case EnemyType.Boss:
                _enemyType = EnemyType.Boss;
                break;
        }
    }
    #endregion
}
