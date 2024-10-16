using System.Collections;
using Sirenix.OdinInspector;
using UnityEngine;

public class DisplayInfo : IDisplayInfo
{
    [SerializeField] private MonoBehaviour _owner;
    [SerializeField] private GameObject    _infoUI;
    [SerializeField] private float         _displayDelay;
    [SerializeField] private LayerMask     _hasInfoMask;
    
    [ReadOnly]
    [SerializeField] private bool          _isStay;
    [ReadOnly]
    [SerializeField] private int           _taskCount;

    public DisplayInfo(MonoBehaviour owner, GameObject infoUI, LayerMask hasInfoMask) : this(owner, infoUI, 2, hasInfoMask){
        _owner       = owner;
        _infoUI      = infoUI;
        _hasInfoMask = hasInfoMask;
    }
    public DisplayInfo(MonoBehaviour owner, GameObject infoUI, float displayDelay, LayerMask hasInfoMask){
        _owner        = owner;
        _infoUI       = infoUI;
        _displayDelay = displayDelay;
        _hasInfoMask  = hasInfoMask;
    }

    public void UpdateRaycast(Vector3 mousePosition){
        var ray = Camera.main.ScreenPointToRay(mousePosition);
        RaycastHit2D hit2D = Physics2D.Raycast( ray.origin, ray.direction, float.MaxValue, _hasInfoMask );
        if(!hit2D.collider){
            if(_isStay){ Undisplay(); }
            return;
        }

        if(!_isStay){ Display(); }
    }

    public void Display(){
        _isStay = true;
        if( _infoUI ){ _owner.StartCoroutine(DisplayTask()); }
        else{ Debug.LogWarning($"Infomation UI shouldn't be {_infoUI}"); }
    }

    public void Undisplay(){
        _isStay = false;
        if(_infoUI){ _infoUI.SetActive(false); }
        else{ Debug.LogWarning($"Infomation UI shouldn't be {_infoUI}"); }
    }

    private IEnumerator DisplayTask(){
        _taskCount++;
        yield return new WaitForSeconds( _displayDelay );
        _taskCount--;
        if( _taskCount == 0 && _isStay ){ _infoUI.SetActive(true); }
    }
}

public interface IDisplayInfo
{
    public void UpdateRaycast(Vector3 mousePosition);
    public void Display();
    public void Undisplay();
}