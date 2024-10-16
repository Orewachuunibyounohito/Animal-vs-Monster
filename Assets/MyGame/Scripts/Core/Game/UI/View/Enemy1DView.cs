using UnityEngine;

public class Enemy1DView : MonoBehaviour
{
    public void UpdateEnemyAnimator( float horzontal ){
        // Debug.Log( "In Enemy one dimension view." );
        if( horzontal >= 0 ){
            GetComponentInChildren<SpriteRenderer>().flipX = false;
        }else{
            GetComponentInChildren<SpriteRenderer>().flipX = true;
        }
    }
}
