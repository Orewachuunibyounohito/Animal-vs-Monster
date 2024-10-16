using System;
using Cinemachine;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;

public class ZoomInOut : MonoBehaviour
{   
    [Serializable]
    public class FloatRange{
        [HorizontalGroup("Split", LabelWidth = 40, MarginRight = 20)]
        public float min;
        [HorizontalGroup("Split")]
        public float max;

        public FloatRange(float min, float max){
            this.max = max;
            this.min = min;
        }
    }

    #region Field
    [TitleGroup("Source", Alignment = TitleAlignments.Centered), LabelWidth(120)]
    [SerializeField] private CinemachineVirtualCamera _virtualCamera;

    [LabelText("Current Size")]
    [TitleGroup("Ortho Settings", Alignment = TitleAlignments.Centered), LabelWidth(120)]
    [PropertyRange("@_cameraOrthoRange.min", "@_cameraOrthoRange.max")]
    [SerializeField] private float _cameraOrthoSize;

    [HideInInspector]
    [LabelText("Range")]
    [TitleGroup("Ortho Settings")]
    [SerializeField] private FloatRange _cameraOrthoRange;

    [TitleGroup("Zoom Multiplier", Alignment = TitleAlignments.Centered)]
    [PropertyRange("@_zoomMultiplierRange.min", "@_zoomMultiplierRange.max")]
    [SerializeField] private float      _zoomMultiplier;

    [HideInInspector]
    [SerializeField] private FloatRange _zoomMultiplierRange;
    #endregion

    #region Custom Event
    public Action<float> CameraZoomed;
    public Action        CameraZoomedIn, CameraZoomedOut;

    public Vector2 touch1Position => GameManager.Instance.CustomInput.Camera.ZoomForTouch1.ReadValue<Vector2>();
    public Vector2 touch2Position => GameManager.Instance.CustomInput.Camera.ZoomForTouch2.ReadValue<Vector2>();
    #endregion

    private void Awake(){
        CameraOrthoSizeInitial();
        ZoomMultiplierInitial();
    }


    private void Start(){
        _zoomMultiplier  = _zoomMultiplierRange.max;
    }

    private void ConfinerInitail(){
        var confiner = _virtualCamera.GetComponent<CinemachineConfiner2D>();
        confiner.m_MaxWindowSize = _cameraOrthoSize;
    }

    private void CameraOrthoSizeInitial(){
        _cameraOrthoSize = Camera.main.orthographicSize;
        ConfinerInitail();
        _cameraOrthoRange.max = _cameraOrthoSize * 0.99f;
        _cameraOrthoRange.min = _cameraOrthoSize * 0.5f;
        if( _cameraOrthoRange == default ){
            _cameraOrthoRange = new FloatRange( 0.5f, 1f );
        }
        if (_cameraOrthoSize > _cameraOrthoRange.max){
            _cameraOrthoSize = _cameraOrthoRange.max;
        }
        UpdateVirtualCamera();
    }

    private void ZoomMultiplierInitial(){
        var delta = _cameraOrthoRange.max - _cameraOrthoRange.min;
        _zoomMultiplierRange = new FloatRange(delta * 0.01f, delta * 0.05f);
    }

    void Update(){
        MouseWheel();
        HandleZoomForTouch();    
    }

    #region Mouse Wheel Control
    private void MouseWheel(){
        if( GameManager.Instance.CustomInput.Camera.Zoom.ReadValue<float>().Equals( 0.0f ) ){ return ; }
        // Debug.Log($"{GameManager.Instance.CustomInput.Camera.Zoom.ReadValue<float>()}");

        var scrollDelta = GameManager.Instance.CustomInput.Camera.Zoom.ReadValue<float>() >= 0? 1 : -1;
        float zoomDelta = -1*( scrollDelta * _zoomMultiplier );
        _cameraOrthoSize = Mathf.Clamp( _cameraOrthoSize+zoomDelta, _cameraOrthoRange.min, _cameraOrthoRange.max );
        UpdateVirtualCamera();
    }
    private void HandleZoomForTouch(){
        var originDistance = (touch1Position - touch2Position).magnitude;
        bool activateZoom = touch1Position != null && touch2Position != null;
        if( !activateZoom || originDistance == 0 ){ return ; }
        Debug.Log($"{originDistance}");

        // var scrollDelta = GameManager.Instance.CustomInput.Camera.ZoomForTouch2.ReadValue<Vector2>() >= 0? 1 : -1;
        // float zoomDelta = -1*( scrollDelta * _zoomMultiplier );
        // _cameraOrthoSize = Mathf.Clamp( _cameraOrthoSize+zoomDelta, _cameraOrthoRange.min, _cameraOrthoRange.max );
        // UpdateVirtualCamera();
    }
    private void UpdateVirtualCamera(){
        if( _virtualCamera.m_Lens.OrthographicSize == _cameraOrthoSize ){ return ; }
        else if( _virtualCamera.m_Lens.OrthographicSize > _cameraOrthoSize ){
            // OnZoomIn?.Invoke( _cameraOrthoSize );
        }else{
            // OnZoomOut?.Invoke( _cameraOrthoSize );
        }
        OnCameraZoomed();
        _virtualCamera.m_Lens.OrthographicSize = _cameraOrthoSize;
        _virtualCamera.GetComponent<CinemachineConfiner2D>().InvalidateCache();
    }
    #endregion

    #region Public Methods
    public void OnCameraZoomed() => CameraZoomed?.Invoke( _cameraOrthoSize*5 );
    public void OnCameraZoomedIn() => CameraZoomedIn?.Invoke();
    public void OnCameraZoomedOut() => CameraZoomedOut?.Invoke();
    #endregion
}
