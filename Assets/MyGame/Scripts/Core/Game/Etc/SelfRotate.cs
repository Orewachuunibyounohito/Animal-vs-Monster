using System.Collections;
using UnityEngine;

public class SelfRotate : MonoBehaviour
{   
    #region Field
    [SerializeField] private float _rotateSpeed;

    private const float ROTATE_TIME = 1f/30;
    #endregion

    private void Awake(){
        StartRotate();
    }

    #region Rotate
    private void StartRotate(){
        StartCoroutine( RotateCoroutine() );
    }

    private IEnumerator RotateCoroutine(){
        while( true ){
            yield return new WaitForSeconds( ROTATE_TIME );
            transform.Rotate( Vector3.forward, _rotateSpeed*ROTATE_TIME );
        }
    }
    #endregion
}
