using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent( typeof( Rigidbody2D ) )]
public class FollowMouse : MonoBehaviour
{
    #region Field
    [SerializeField] private float      _speed;
    [SerializeField] private Vector2    _prevMouse;

    private InputAction cameraControlAction;
    #endregion

    #region Property
    public Rigidbody2D rb           => GetComponent<Rigidbody2D>();
    public Vector2     mouseInWorld => Camera.main.ScreenToWorldPoint( GameManager.Instance.CustomInput.Camera.ScreenPosition.ReadValue<Vector2>() );
    #endregion

    private void Awake(){
        if( TryGetComponent( out ZoomInOut zoom ) ){
            zoom.CameraZoomed += AdjustSpeed;
        }
        cameraControlAction = GameManager.Instance.CustomInput.Camera.Control;
    }

    private void FixedUpdate(){
        HandleCameraControl();
    }

    private void HandleCameraControl()
    {
        // Mouse Middie
        if(cameraControlAction.IsPressed()){
            Vector2 displacement = mouseInWorld - _prevMouse;
            rb.MovePosition(Vector2.MoveTowards(transform.position, (Vector2)transform.position - displacement, _speed * Time.fixedDeltaTime));
            _prevMouse = mouseInWorld;
        }else{
            transform.position = new Vector2(Camera.main.transform.position.x, Camera.main.transform.position.y);
        }
    }

    private void AdjustSpeed( float newSpeed ){
        _speed = newSpeed;
    }
}
