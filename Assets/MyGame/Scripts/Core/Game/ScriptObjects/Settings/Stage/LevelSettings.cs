using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "TD/Settings/Stage/Level", fileName = "New Level Settings")]
public class LevelSettings : ScriptableObject
{
    public List<LevelSet> Stages;

    [Serializable]
    public class LevelSet : IEquatable<LevelSet>
    {
        public StageName    StageName;
        public NewLevelData LevelData;

        public override int GetHashCode(){
            return StageName.GetHashCode() ^ LevelData.GetHashCode();
        }

        public bool Equals(LevelSet other){
            return other != null && other.StageName == StageName;
        }
    }
}
