using UnityEngine;

// Not be used
public class AttackExit : StateMachineBehaviour
{
    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Attacker attacker = animator.GetComponent<Attacker>();
        attacker.AttackExit();
    }
}
