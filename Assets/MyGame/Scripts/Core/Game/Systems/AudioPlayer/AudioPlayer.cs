using System.Collections.Generic;
using UnityEngine;

public class AudioPlayer
{
    private const int BGM = 0, SFX = 1; 

    private AudioSource[] _channels;
    private Dictionary<BgmName, AudioClip> _bgms;
    private Dictionary<SfxName, AudioClip> _sfxes;

    public AudioSource BgmChannel => _channels[BGM];
    public AudioSource SfxChannel => _channels[SFX];
    public Dictionary<BgmName, AudioClip> Bgms  => _bgms;
    public Dictionary<SfxName, AudioClip> Sfxes => _sfxes;

    public AudioPlayer(GameManager gameManager, AudioSettings audioSo){
        _channels = new AudioSource[2];
        _channels[BGM] = gameManager.gameObject.AddComponent<AudioSource>();
        _channels[SFX] = gameManager.gameObject.AddComponent<AudioSource>();
        _channels[BGM].loop = true;

        _bgms = new Dictionary<BgmName, AudioClip>();
        foreach(var bgm in audioSo.BgmList){
            _bgms.Add(bgm.Name, bgm.Clip);
        }
        _sfxes = new Dictionary<SfxName, AudioClip>();
        foreach(var sfx in audioSo.SfxList){
            _sfxes.Add(sfx.Name, sfx.Clip);
        }
        
        DefaultVolume();
    }

    public void PlayBgm(BgmName bgmName){
        _channels[BGM].clip = _bgms[bgmName];
        _channels[BGM].Play();
    }
    public void PlaySfx(SfxName sfxName) =>
        _channels[SFX].PlayOneShot(_sfxes[sfxName]);

    private void DefaultVolume(){
        foreach(var channel in _channels){ channel.volume = 0.1f; }
    }
}
