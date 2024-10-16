using UnityEngine;

public class DestroyByDistance : MonoBehaviour
{
    #region Field
    [SerializeField] private float   _distance;
    [SerializeField] private Vector2 _positionOrigin;

    [SerializeField] private Vector2 _prevPosition;
    [SerializeField] private float   _grandTotal;
    #endregion

    #region Property
    public float distance   => _distance;
    public float grandTotal => _grandTotal;
    #endregion

    private void FixedUpdate(){
        _grandTotal += Vector2.Distance( transform.position, _prevPosition );
        _prevPosition = transform.position;
        if( _grandTotal >= _distance ){
            Destroy( gameObject );
        }

        // if( Vector2.Distance( transform.position, _positionOrigin ) > _distance ){
        //     Destroy( gameObject );
        // }
    }

    #region Set Function
    public void Initialize( float distance, Vector2 source ){
        SetDistance( distance );
        SetOrigin( source );
        _prevPosition = source;
    }
    public void SetDistance( float distance ) => _distance = distance;
    public void SetOrigin( Vector2 source )   => _positionOrigin = source;
    #endregion
}
