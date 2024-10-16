using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent( typeof( BoxCollider2D ) )]
public class EnterHurtArea : MonoBehaviour
{   
    #region Field
    private Vector2 prevPosition;
    
    [ReadOnly]
    [SerializeField] private LayerMask hurtMask;
    #endregion

    #region Const
    private const string HURT_LAYERMASK = "HurtArea";
    #endregion

    #region Event
    public UnityEvent OnEnter;
    #endregion

    private void Awake(){
        prevPosition = transform.position;
        hurtMask     = LayerMask.NameToLayer(HURT_LAYERMASK);
    }
    
    private void OnTriggerEnter2D(Collider2D other){
        if(other?.gameObject.layer == hurtMask.value){
            Destroy( gameObject );
            OnEnter?.Invoke();
        }

        prevPosition = transform.position;
    }
}
