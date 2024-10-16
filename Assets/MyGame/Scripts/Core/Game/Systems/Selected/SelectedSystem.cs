using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

public class SelectedSystem
{
    public ISelectableComponent SelectedComponent{ get; private set; }

    public void Selecting(InputAction.CallbackContext callback){
        if(callback.performed){
            var ray           = Camera.main.ScreenPointToRay(GameManager.Instance.CustomInput.Gameplay.ScreenPosition.ReadValue<Vector2>());
            var selectedLayer = LayerMask.GetMask("UI", "Tower", "TowerItem", "Enemy", "InventoryItem", "Menu");
            var hit2D         = Physics2D.GetRayIntersection(ray, float.MaxValue, selectedLayer);
            if(LayerMask.NameToLayer("Menu") == hit2D.collider?.gameObject.layer){ return ; }
            var newSelectedComponent = hit2D.collider?.GetComponent<ISelectableComponent>();
            if(SelectedComponent == newSelectedComponent){ return ; }
            SelectedComponent?.Deselect();
            SelectedComponent = newSelectedComponent;
            SelectedComponent?.Select();
        }
    }
    public void SelectingForTouch(InputAction.CallbackContext callback){
        if(callback.performed){
            var ray           = Camera.main.ScreenPointToRay(GameManager.Instance.CustomInput.Gameplay.ScreenPosition.ReadValue<Vector2>());
            var selectedLayer = LayerMask.GetMask("UI", "Tower", "TowerItem", "Enemy", "InventoryItem", "Menu");
            var hit2D         = Physics2D.GetRayIntersection(ray, float.MaxValue, selectedLayer);
            if(hit2D.collider != null){
                GameManager.Instance.CustomInput.Camera.Disable();
                Debug.Log($"Exit Camera.");
            }
            if(LayerMask.NameToLayer("Menu") == hit2D.collider?.gameObject.layer){ return ; }
            var newSelectedComponent = hit2D.collider?.GetComponent<ISelectableComponent>();
            if(SelectedComponent == newSelectedComponent){ return ; }
            SelectedComponent?.Deselect();
            SelectedComponent = newSelectedComponent;
            SelectedComponent?.Select();
        }
    }

    public void LostSelected(){
        SelectedComponent = null;
    }
}
