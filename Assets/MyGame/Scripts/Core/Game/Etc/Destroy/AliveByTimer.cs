using System.Collections;
using UnityEngine;

public class AliveByTimer : MonoBehaviour
{
    #region Field
    [SerializeField] private float _aliveDuration;

    private const float COUNT_DOWN_TIME = 1f/30;
    #endregion

    private void Awake(){
        StartCountDown();
    }
    
    #region Destroy Self when Timeout
    private void StartCountDown(){
        StartCoroutine( CountDownCoroutine() );
    }
    private IEnumerator CountDownCoroutine(){
        float timer = _aliveDuration;
        while( true ){
            yield return new WaitForSeconds( COUNT_DOWN_TIME );
            timer = Mathf.Clamp( timer-COUNT_DOWN_TIME, 0f, _aliveDuration );
            if( timer == 0 ){
                Destroy( gameObject );
                yield break;
            }
        }
    }
    #endregion
}
