using UnityEngine;

public class GameplayUIAnimate
{
    private Animator animator;

    public GameplayUIAnimate(Animator animator){
        this.animator = animator;
    }

    public void InfoPanelExtend(){
        var normalizedTime = animator.GetCurrentAnimatorStateInfo(0).normalizedTime;
        // if(animator.GetCurrentAnimatorStateInfo(0).IsName("None")){
        //     animator.Play("Extend", 0, animator.transform.localScale.x);
        // }else{
        //     animator.Play("Extend", 0, 1 - normalizedTime);
        // }
        var playingProgress = animator.transform.localScale.x;
        playingProgress = playingProgress == 1? 0.99f : playingProgress;
        animator.Play("Extend", 0, playingProgress);
    }
    public void InfoPanelFold(){
        var normalizedTime = animator.GetCurrentAnimatorStateInfo(0).normalizedTime;
        // if(animator.GetCurrentAnimatorStateInfo(0).IsName("None")){
        //     animator.Play("Fold", 0, 1 - animator.transform.localScale.x);
        // }else{
        //     animator.Play("Fold", 0, 1 - normalizedTime);
        // }
        var playingProgress = 1 - animator.transform.localScale.x;
        playingProgress = playingProgress == 1? 0.99f : playingProgress;
        animator.Play("Fold", 0, playingProgress);
    }
}