using System.Collections.Generic;
using UnityEngine;

public class PlayerAudio : MonoBehaviour
{
    [SerializeField]
    private List<SfxName> hurtAudios;

    public void PlayHurtAudio(){
        int randomIndex = Random.Range(0, hurtAudios.Count);
        GameManager.Instance.AudioPlayer.PlaySfx(hurtAudios[randomIndex]);
    }
}