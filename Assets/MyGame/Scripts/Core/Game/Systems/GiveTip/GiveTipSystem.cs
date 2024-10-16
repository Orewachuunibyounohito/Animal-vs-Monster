using System.Collections;
using UnityEngine;

public class GiveTipSystem
{
    public int   Tip{ get; set; } = 1;
    public float Period{ get; set; } = 1;

    private NewPlayer _player;
    private bool _working   = false;
    private bool _isGiveTip = false;

    public GiveTipSystem(NewPlayer player, int tip = 1){
        _player = player;
        Tip     = tip;
    }

    public void Start(){
        if(_working){ return ; }
        else{
            _isGiveTip = true;
            _player.StartCoroutine(GiveTipTask());
        }
    }
    public void Stop() => _isGiveTip = false;

    IEnumerator GiveTipTask(){
        while(_isGiveTip){
            _player.MakeMoney(Tip);
            yield return new WaitForSeconds(Period);
        }
        _working = false;
    }        
}