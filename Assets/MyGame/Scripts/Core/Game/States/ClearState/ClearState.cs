
using MyScripts;

public class ClearState : State
{
    public override void Enter(){
        base.Enter();
        animator.Play( _motion.name );
    }
    public override void Do(){
        // not be used
        if( time >= 1f || isComplete ){
            isComplete = true;
            gameObject.SetActive( false );
            return ;
        }
        animator.Play( _motion.name, 0, time );
    }
    public override void FixDo(){}
    public override void Exit(){}
}
