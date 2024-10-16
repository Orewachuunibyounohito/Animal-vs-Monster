using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class FunctionalPanelView
{
    [SerializeField]
    private Button _itemUseButton, _itemSellButton, _towerUpgradeButton, _towerSellButton;

    public Transform ItemMenu{ get; private set; }
    public Transform TowerMenu{ get; private set; }

    public Button.ButtonClickedEvent UsingItem, SellingItem, UpgragingTower, SellingTower;
    
    public FunctionalPanelView(Transform presenter){
        ItemMenu = presenter.Find("ItemMenu");
        var itemButtons = ItemMenu.GetComponentsInChildren<Button>(true);
        _itemUseButton  = itemButtons[0];
        _itemSellButton = itemButtons[1];
        UsingItem   = _itemUseButton.onClick;
        SellingItem = _itemSellButton.onClick;

        TowerMenu = presenter.Find("TowerMenu");
        var towerButtons    = TowerMenu.GetComponentsInChildren<Button>(true);
        _towerUpgradeButton = towerButtons[0];
        _towerSellButton    = towerButtons[1];
        UpgragingTower = _towerUpgradeButton.onClick;
        SellingTower   = _towerSellButton.onClick;
    }
    
    public void ShowItemMenu()  => ItemMenu.gameObject.SetActive(true);
    public void ShowTowerMenu() => TowerMenu.gameObject.SetActive(true);
    public void HideItemMenu()  => ItemMenu.gameObject.SetActive(false);
    public void HideTowerMenu() => TowerMenu.gameObject.SetActive(false);
    public void NonUpgrade()    => _towerUpgradeButton.interactable = false;
}
