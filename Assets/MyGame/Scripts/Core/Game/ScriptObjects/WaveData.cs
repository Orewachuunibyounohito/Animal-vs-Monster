using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu( fileName = "NewWave", menuName = "TD/Wave Data" )]
public class WaveData : ScriptableObject
{
    #region Field
    [SerializeField] private List<EnemyData> _enemyDatas;
    [SerializeField] private List<int>       _counts;
    [SerializeField] private float          _spawnTime, _spawnRange;
    #endregion

    #region Property
    public float spwanTime  => _spawnTime;
    public float spwanRange => _spawnRange;
    public int   kindCount  => _enemyDatas.Count;
    public int   TotalEnemyCount{
        get{
            int total = 0;
            foreach( var count in _counts ){
                total += count;
            }
            return total;
        }
    }
    public Dictionary<EnemyData, int> allWaveData{
        get{ 
            Dictionary<EnemyData, int> wave = new Dictionary<EnemyData, int>();
            for( int idx = 0; idx < _enemyDatas.Count; idx++ ){
                if( idx >= _counts.Count ){
                    wave.Add( _enemyDatas[idx], 0 );
                }else{
                    wave.Add( _enemyDatas[idx], _counts[idx] );
                }
            }
            return wave;
        }
    }

    public GameObject GetEnemyPrefab( int index ) => _enemyDatas[index].enemyPrefab;

    public KeyValuePair<EnemyData, int> GetWaveData( int index ){
        if( index >= _counts.Count ){
            return new KeyValuePair<EnemyData, int>( _enemyDatas[index], 0 );
        }
        return new KeyValuePair<EnemyData, int>( _enemyDatas[index], _counts[index] );
    }
    #endregion
}
