using UnityEngine;

public class Inactive : StateMachineBehaviour
{
    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if( GameManager.Instance.SpawnEnemy.IsStageClear ){
            GameManager.Instance.StageClear();
        }else{
            GameManager.Instance.NextWave();
        }
        animator.gameObject.SetActive( false );
    }
}
