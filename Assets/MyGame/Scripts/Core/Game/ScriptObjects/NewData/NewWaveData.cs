using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Sirenix.OdinInspector;
using UnityEngine;

[CreateAssetMenu( fileName = "NewWave", menuName = "TD/New Wave Data" )]
public class NewWaveData : ScriptableObject, IEnumerable<NewWaveData.WaveDataSet>
{
    #region Field
    [LabelText("Enemies")]
    [SerializeField] private List<WaveDataSet> _waveDataSet;

    [SerializeField] private float              _spawnTime, _spawnRange;
    #endregion

    #region Property
    public WaveDataSet this[int index] => _waveDataSet[index];

    public float spwanTime  => _spawnTime;
    public float spwanRange => _spawnRange;
    public int   kindCount  => _waveDataSet.Count;
    public int   TotalEnemyCount{
        get{
            int total = 0;
            foreach( var data in _waveDataSet ){
                total += data.enemyCount;
            }
            return total;
        }
    }
    #endregion

    public GameObject GetEnemyPrefab( int index ) => _waveDataSet[index].enmeyData.prefab;

    public WaveDataSet GetWaveData( int index ) => _waveDataSet[index];

    #region Help Function
    public void AssignFromOldData( WaveData data ){
        _spawnTime  = data.spwanTime;
        _spawnRange = data.spwanRange;
    }

    public IEnumerator<WaveDataSet> GetEnumerator(){
        return _waveDataSet.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator(){
        return _waveDataSet.GetEnumerator();
    }
    #endregion

    [Serializable]
    public class WaveDataSet
    {
        [InlineEditor]
        [LabelText("Enemy")]
        [HorizontalGroup("Split", LabelWidth = 70, MarginRight = 10)]
        public NewEnemyData enmeyData;
        [LabelText("Count")]
        [HorizontalGroup("Split")]
        public int          enemyCount;
    }
}
