using NUnit.Framework;
using UnityEngine;

public class AudioPlayerTest
{
    private GameManager   gameManager;
    private AudioSettings audioSo;
    private AudioPlayer   audioPlayer;

    [SetUp]
    public void SetUp(){
        gameManager = new GameObject("GameManager").AddComponent<GameManager>();
        audioSo     = Resources.Load<AudioSettings>("TD/Audios/AudioSettings");
        audioPlayer = new AudioPlayer(gameManager, audioSo);
    }

    [Category("Audio")]
    [Test]
    public void NewAudioPlayerBgmDictionaryIsCorrect(){
        var bgmDictionaryCorrect = true;

        foreach(var bgm in audioSo.BgmList){
            if(bgm.Clip != audioPlayer.Bgms[bgm.Name]){
                bgmDictionaryCorrect = false;
                break;
            }
        }

        Assert.IsTrue(bgmDictionaryCorrect);
    }

    [Category("Audio")]
    [Test]
    public void NewAudioPlayerSfxDictionaryIsCorrect(){
        var sfxDictionaryCorrect = true;

        foreach(var sfx in audioSo.SfxList){
            if(sfx.Clip != audioPlayer.Sfxes[sfx.Name]){
                sfxDictionaryCorrect = false;
                break;
            }
        }

        Assert.IsTrue(sfxDictionaryCorrect);
    }

    [Category("Audio")]
    [Test]
    [TestCase(BgmName.Menu)]
    [TestCase(BgmName.WorldMap)]
    [TestCase(BgmName.Battle)]
    public void PlayBgmThenClipMatchAudioSo(BgmName bgmName){
        
        audioPlayer.PlayBgm(bgmName);

        var actual   = audioPlayer.BgmChannel.clip;
        var expected = audioSo.BgmList.Find((bgmSet) => bgmSet.Name == bgmName ).Clip;
        
        Assert.AreEqual(expected, actual);
    }
}