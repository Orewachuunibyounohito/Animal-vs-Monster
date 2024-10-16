using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

[CreateAssetMenu(menuName = "TD/Settings/Audio/BGM", fileName = "New BGM Settings")]
public class BgmSettings : ScriptableObject
{
    // [InlineEditor]
    public List<BgmSet> BgmList;
    
    [Serializable]
    public class BgmSet
    {
        public BgmName   Name;
        public AudioClip Clip;
    }
}
