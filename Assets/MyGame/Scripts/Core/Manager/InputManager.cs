using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class InputManager : Singleton<InputManager>
{
    #region Field
    private GameObject selectedTower = default;
    private LayerMask  towerMask;

    public  UnityEvent<int> OnSellTower;
    #endregion

    protected override void Awake() {
        base.Awake();
        towerMask = LayerMask.GetMask( "Tower" );
    }

    // private void Update() => MouseClick();

    #region Select Tower
    private void MouseClick(){
        if( Mouse.current.leftButton.wasPressedThisFrame ){
            SelectTower( Mouse.current.position.ReadValue() );
        }else if( Mouse.current.rightButton.wasPressedThisFrame ){
            // SellTower();
        }
    }
    private void SelectTower( Vector2 position ){
        Ray          ray   = Camera.main.ScreenPointToRay( position );
        RaycastHit2D hit2D = Physics2D.Raycast( ray.GetPoint( 0 ), ray.direction, 20f, towerMask );

        if( selectedTower == hit2D.collider?.gameObject ){ return ; }
        if( hit2D.collider == null){
            Deselect();
            return;
        }

        selectedTower = hit2D.collider.gameObject;
        if( selectedTower.TryGetComponent( out NewAttacker newAttacker ) ){
            newAttacker.ToSelected();
        }
    }
    private void Deselect(){
        if( selectedTower == default ){ return ; }
        if( selectedTower.TryGetComponent( out NewAttacker newAttacker ) ){
            newAttacker.ToDeselected();
        }
        selectedTower = default;
    }
    private void SellTower(){
        if( selectedTower == default ){ return ; }

        var tower = GameManager.Instance.Library.GetData<NewTowerData>(selectedTower.name);
        OnSellTower.Invoke( tower.cost );

        Destroy( selectedTower );
        selectedTower = default;
    }
    #endregion

    #region Test Function
    private void TowerShoot(){
        if( selectedTower == default ){ return ; }

        selectedTower.GetComponent<Weapon>().DoAttack();
    }
    #endregion 

}
