using UnityEngine;

public abstract class AttackerState : State
{
    #region Field

    #endregion

    public override void Enter(){
        base.Enter();
    }
    public override void Do(){
        // do something

        animator.Play( motionName, 0, time );
        if( time >= 1f ){
            isComplete = true;
        }
    }
    public override void FixDo(){}
    public override void Exit(){}
}
