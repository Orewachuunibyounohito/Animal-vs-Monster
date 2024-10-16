using System.Collections;
using UnityEngine;

public class Death : MonoBehaviour
{
    #region Const
    private const float DIE_TO_DESTROY_TIME = 3.5f;
    #endregion

    private void Start(){
        if( TryGetComponent( out Enemy enemy ) ){
            enemy.OnDie.AddListener( Die );
        }
        if( TryGetComponent( out NewEnemy newEnemy ) ){
            newEnemy.CreatureDied.AddListener( Die );
        }
    }

    #region Die Function
    public void Die(){
        StartCoroutine( DieCoroutine() );
    }

    private IEnumerator DieCoroutine(){
        gameObject.layer = LayerMask.GetMask( "Nothing" );
        GetComponent<Rigidbody2D>().simulated = false;
        if( TryGetComponent( out Animator animator ) ){
            animator.enabled = false;
        }
        yield return new WaitForSeconds( DIE_TO_DESTROY_TIME );
        Destroy( gameObject );
    }
    #endregion

}
