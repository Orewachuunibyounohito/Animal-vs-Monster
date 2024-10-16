using System.Collections;
using System.Collections.Generic;
using TD.Info;
using UnityEngine;
using UnityEngine.Events;

public class SpawnEnemy : MonoBehaviour
{
    #region Field
    // [SerializeField] private LevelData _levelData;
    [SerializeField] private NewLevelData _levelData;
    [SerializeField] private Transform    _enemyCollection;
    [SerializeField] private int          _currWave = -1;
    [SerializeField] private Transform    _spawnPointsCollection, _enemyPathTransCollection;

    [SerializeField] private List<Vector2> _spawnPoints, _enemyPath;

    private List<int> enemiesIndex = new List<int>();
    #endregion

    #region Property
    public int        EnemyCount   => enemiesIndex.Count;
    public bool       IsStageClear => _currWave == _levelData.Count-1;
    public string     WaveText     => $"{_currWave+1}/{_levelData.Count}";
    public RewardData LevelReward  => _levelData.clearReward;
    #endregion

    #region Const
    private const float  SPAWN_SPEED             = 0.5f;
    private const float  SPAWN_SPEED_RANGE       = 0.5f;
    private const int    RANDOM_TIMES            = 500;
    private const string SPAWN_POINTS_COLLECTION = "SpawnPoints";
    private const string ENEMY_PATH_COLLECTION   = "EnemyPath";
    #endregion

    #region Event
    public UnityEvent<SpawnEnemy> OnSpawnEnemy;
    public UnityEvent             StageCleared;
    #endregion

    private void Awake(){
        _enemyCollection = new GameObject( "Enemy Collection" ).transform;
        SetSpawnPoint();
        SetEnemyPath();
    }
    private void Start(){
    }

    #region Spawn
    public void NextSpawn(){
        _currWave++;
        StartSpawn();
    }
    public void StartSpawn(){
        GenerateEnemiesOrder();
        // RandomSwap( ref enemiesIndex, RANDOM_TIMES );
        enemiesIndex = RandomSwap( enemiesIndex, RANDOM_TIMES );
        OnSpawnEnemy.Invoke(this);
        StartCoroutine( NewSpawnCoroutine() );
    }
    IEnumerator NewSpawnCoroutine(){
        float      spawnTime;
        int        pointIndex;
        GameObject prefab;
        NewEnemy   enemy;
        
        foreach( var index in enemiesIndex ){
            spawnTime =  _levelData[_currWave].spwanTime+Random.Range( 0, _levelData[_currWave].spwanRange );
            yield return new WaitForSeconds( spawnTime );
            pointIndex = Random.Range( 0, _spawnPoints.Count );
            prefab     = _levelData[_currWave].GetEnemyPrefab( index );
            enemy      = Instantiate( prefab.GetComponent<NewEnemy>(), _spawnPoints[pointIndex], prefab.transform.rotation, _enemyCollection );
            enemy.Initialize( _enemyPath );
            enemy.gameObject.AddComponent<SelectedEnemy>();
        }
    }
    #endregion

    #region Generate Enemies Spawn Order
    private void GenerateEnemiesOrder(){
        enemiesIndex.Clear();
        for( int idx = 0, amount; idx < _levelData[_currWave].kindCount; idx++ ){
            amount = _levelData[_currWave].GetWaveData(idx).enemyCount;

            for( ; amount > 0; amount-- ){
                enemiesIndex.Add( idx );
            }
        }
    }
    #endregion

    #region Set Spawn Point
    private void SetSpawnPoint(){
        _spawnPoints = new List<Vector2>();
        foreach( var pointTrans in _spawnPointsCollection.GetComponentsInChildren<Transform>() ){
            if( pointTrans.name.Equals( SPAWN_POINTS_COLLECTION ) ){
                continue;
            }

            _spawnPoints.Add( pointTrans.position );
        }
    }
    #endregion

    #region Set Enemy Path
    private void SetEnemyPath(){
        _enemyPath = new List<Vector2>();
        foreach( var pointTrans in _enemyPathTransCollection.GetComponentsInChildren<Transform>() ){
            if( pointTrans.name.Equals( ENEMY_PATH_COLLECTION ) ){
                continue;
            }

            _enemyPath.Add( pointTrans.position );
        }
    }
    #endregion

    public void SetLevelData(NewLevelData levelData) => _levelData = levelData;

    #region Help Function
    private void ListSwap<T>( ref List<T> source, int index1, int index2 ){
        T temp = source[index1];
        source[index1] = source[index2];
        source[index2] = temp;
    }
    private List<T> ListSwap<T>( List<T> source, int index1, int index2 ){
        T temp = source[index1];
        source[index1] = source[index2];
        source[index2] = temp;
        return source;
    }

    private void RandomSwap<T>( ref List<T> source, int times ){
        if( source.Count < 2 ) return ;

        (int, int) indexes;
        while( times > 0 ){
            indexes = RandomNoRepeat( 0, source.Count );
            ListSwap( ref source, indexes.Item1, indexes.Item2 );
            times--;
        }
    }
    private List<T> RandomSwap<T>( List<T> source, int times ){
        if( source.Count < 2 ) return source;

        (int, int) indexes;
        while( times > 0 ){
            indexes = RandomNoRepeat( 0, source.Count );
            source = ListSwap( source, indexes.Item1, indexes.Item2 );
            times--;
        }
        return source;
    }

    // maxExclusive
    private (int, int) RandomNoRepeat( int min, int max ){
        int int1 = Random.Range( min, max );
        int int2 = Random.Range( min, max );

        if( min > max ){
            int temp = min;
            min = max;
            max = temp;
        }else if( min == max ){
            return (min, max);
        }

        while( int1 == int2 ){
            int2 = Random.Range( min, max );
        }
        return (int1, int2);
    }

    private void ShowEnemiesIndex(){
        string log = "[";
        enemiesIndex.ForEach( ( index ) => log += index+", " );
        log = log.Substring( 0, log.Length-2 )+"]";
        Debug.Log( $"Enemies Index: \n{log}" );
    }
    
    public void AssignPath( ref List<Vector2> source ){
        source = _enemyPath;
    }
    #endregion

}