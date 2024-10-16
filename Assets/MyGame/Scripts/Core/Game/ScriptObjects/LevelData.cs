using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu( fileName = "NewLevel", menuName = "TD/Level Data", order = 3 )]
public class LevelData : ScriptableObject
{
    #region Field
    [SerializeField] private List<WaveData> _waveDatas;
    [SerializeField] private RewardData     _clearReward;
    #endregion

    #region Property
    public WaveData   this[int waveIndex] => _waveDatas[waveIndex];
    public int        Count               => _waveDatas.Count;
    public RewardData clearReward         => _clearReward;
    #endregion

}
