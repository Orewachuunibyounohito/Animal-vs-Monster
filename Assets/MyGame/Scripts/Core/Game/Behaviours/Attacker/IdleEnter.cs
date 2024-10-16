using UnityEngine;

public class IdleEnter : StateMachineBehaviour
{
    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if( animator.TryGetComponent( out MeleeAttacker attacker ) ){
            if(attacker.IsIdle){ return ; }
            attacker.ToIdle();
        }
    }
}
