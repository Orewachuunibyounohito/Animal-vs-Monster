using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

[CreateAssetMenu(menuName = "TD/Settings/Audio/Audio", fileName = "New Audio Settings")]
public class AudioSettings : ScriptableObject
{
    [SerializeField]
    [InlineEditor]
    private BgmSettings bgmSettings;
    [SerializeField]
    [InlineEditor]
    private SfxSettings sfxSettings;

    public List<SfxSettings.SfxSet> SfxList =>
        sfxSettings.SfxList;
    public List<BgmSettings.BgmSet> BgmList =>
        bgmSettings.BgmList;
}
