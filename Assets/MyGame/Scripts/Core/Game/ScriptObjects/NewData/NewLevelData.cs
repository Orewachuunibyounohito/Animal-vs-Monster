using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

[CreateAssetMenu( fileName = "NewLevel", menuName = "TD/New Level Data", order = 3 )]
public class NewLevelData : ScriptableObject
{
    #region Field
    [InlineEditor]
    [SerializeField] private List<NewWaveData> _waveDatas;
    [InlineEditor]
    [SerializeField] private RewardData        _clearReward;
    #endregion

    #region Property
    public NewWaveData this[int waveIndex]    => _waveDatas[waveIndex];
    public int         Count                  => _waveDatas.Count;
    public RewardData  clearReward            => _clearReward;
    #endregion

}
