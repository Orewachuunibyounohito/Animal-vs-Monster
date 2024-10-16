using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

[CreateAssetMenu(menuName = "TD/Settings/Audio/SFX", fileName = "New SFX Settings")]
public class SfxSettings : ScriptableObject
{
    // [InlineEditor]
    public List<SfxSet> SfxList;

    [Serializable]
    public class SfxSet
    {
        public SfxName   Name;
        public AudioClip Clip;
    }
}
