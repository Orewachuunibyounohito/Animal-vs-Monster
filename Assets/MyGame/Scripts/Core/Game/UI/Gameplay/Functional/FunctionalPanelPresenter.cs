using System;
using TD.Item;
using UnityEngine;

public class FunctionalPanelPresenter : MonoBehaviour
{   
    [SerializeField]
    private FunctionalPanelView view;

    void Awake(){
        view = new FunctionalPanelView(transform);
    }

    public void ShowItemMenu()  => view.ShowItemMenu();
    public void ShowTowerMenu() => view.ShowTowerMenu();    
    public void HideItemMenu()  => view.HideItemMenu();
    public void HideTowerMenu() => view.HideTowerMenu();

    public void SetItemPanel(SelectedItem selected){
        view.UsingItem.AddListener(selected.Use);
        view.UsingItem.AddListener(selected.Deselect);
        view.SellingItem.AddListener(selected.Sell);
        view.SellingItem.AddListener(selected.Deselect);
    }

    public void UnsetItemPanel(SelectedItem selected){
        view.UsingItem.RemoveListener(selected.Use);
        view.UsingItem.RemoveListener(selected.Deselect);
        view.SellingItem.RemoveListener(selected.Sell);
        view.SellingItem.RemoveListener(selected.Deselect);
    }

    public void SetTowerPanel(SelectedTower selected)
    {
        view.UpgragingTower.AddListener(selected.Upgrade);
        view.UpgragingTower.AddListener(selected.Deselect);
        view.SellingTower.AddListener(selected.Sell);
        view.SellingTower.AddListener(selected.Deselect);
    }

    public void UnsetTowerPanel(SelectedTower selected)
    {
        view.UpgragingTower.RemoveListener(selected.Upgrade);
        view.UpgragingTower.RemoveListener(selected.Deselect);
        view.SellingTower.RemoveListener(selected.Sell);
        view.SellingTower.RemoveListener(selected.Deselect);
    }

    public Rect GetItemMeunRect() => view.ItemMenu.GetComponent<RectTransform>().rect;
    public Rect GetTowerMeunRect() => view.TowerMenu.GetComponent<RectTransform>().rect;
    public void MoveToNewPosition(Vector3 newPosition, Rect rect , Direction direction){
        switch(direction){
            case Direction.Up:    newPosition += new Vector3(0, rect.height / 100, 0); break;
            case Direction.Left:  newPosition += new Vector3(-rect.width / 100, 0, 0); break;
            case Direction.Down:  newPosition += new Vector3(0, -rect.height / 100, 0); break;
            case Direction.Right: newPosition += new Vector3(rect.width / 100, 0, 0); break;
        }
        newPosition.z      = transform.position.z;
        transform.position = newPosition;
    }

    public void NonUpgrade() => view.NonUpgrade();

    public enum Direction
    {
        Up,
        Left,
        Down,
        Right
    }
}
