using System.Collections;
using UnityEngine;

public class DestroyByNoMove : MonoBehaviour
{
    #region Private Variable
    [SerializeField] private int noMoveCount;
    
    private Vector2 prevPosition;
    #endregion

    #region Unity Events
    private void Start(){
        StartCoroutine( NoMoveCountdownTask() );
    }
    #endregion

    #region No Move Countdown Task
    private IEnumerator NoMoveCountdownTask(){
        float checkInterval = 0.1f;
        int   counter = 0;
        prevPosition = transform.position;
        while( counter < noMoveCount ){
            yield return new WaitForSeconds( checkInterval );
            if( prevPosition.Equals( transform.position ) ){ counter++; }
            else                                           { counter = 0; }
            prevPosition = transform.position;
        }
        Destroy( gameObject );
    }
    #endregion
}
