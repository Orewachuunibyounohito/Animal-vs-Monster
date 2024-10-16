using UnityEngine;

[CreateAssetMenu( fileName = "EnemyData", menuName = "TD/Enemy Data", order = 2 )]
public class EnemyData : ScriptableObject
{
    #region Field
    [SerializeField] private GameObject _enemyPrefab;
    [SerializeField] private int        _maxHp;
    [SerializeField] private int        _defense;
    [SerializeField] private float      _speed;
    [SerializeField] private ulong      _strength;
    [SerializeField] private RewardData _reward;
    [SerializeField] private EnemyType  _enemyType;
    #endregion

    #region Property
    public GameObject enemyPrefab => _enemyPrefab;
    public int        maxHp       => _maxHp;
    public int        defense     => _defense;
    public float      speed       => _speed;
    public ulong      strength    => _strength;
    public RewardData reward      => _reward;
    public EnemyType  enemyType   => _enemyType;
    #endregion

}
