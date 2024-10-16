using System;
using Sirenix.OdinInspector;
using UnityEngine;

// GameObject version
public class ShowInfoGO : MonoBehaviour
{
    public enum TargetLayer{ Tower, TowerItem, Enemy }

    public LayerMask targetMask;
    public int layerNumTest;

    [ShowInInspector]
    public IInfoUIPanel infoUI;

    private void Start(){
        infoUI = GameObject.Find("GameplayUI")
                           .GetComponent<GameplayUIPanel>()
                           .GetInfoPanel();
    }

    private void OnTriggerEnter2D(Collider2D other){
        OnEnterHasInfoTarget(other);
    }
    private void OnTriggerExit2D(Collider2D other){
        OnExitHasInfoTarget(other);
    }

    private void OnEnterHasInfoTarget(Collider2D other){
        if(other.gameObject.layer == LayerMask.NameToLayer(TargetLayer.Tower.ToString())){
            var info = other.GetComponent<IInfoData>();
            // infoUI.Name = info.Name;
            // infoUI.SetInfo(info.Detail);
            infoUI.SetInfo(info);
            infoUI.ShowInfo();
        }
    }

    private void OnExitHasInfoTarget(Collider2D other){
        if(other.gameObject.layer == LayerMask.NameToLayer(TargetLayer.Tower.ToString())){
            // infoUI.Name = "";
            // infoUI.SetInfo(new string[]{"", ""});
            infoUI.HideInfo();
        }
    }

    #region Helper Methods
    [Button]
    public void IntShift(){
        Debug.Log($"{1 << layerNumTest}");
    }
    #endregion
}
