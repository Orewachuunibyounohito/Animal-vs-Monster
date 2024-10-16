using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectedTower : SelectableComponenet, IInfoData
{
    public string   Name { get; private set; }
    public string[] Detail { get; private set; }

    private NewTowerData             upgradedTower;
    private GameObject               attackArea;
    private FunctionalPanelPresenter menuPresenter;

    public event Action SellingTower;

    void Awake(){
        var towerData = GameManager.Instance.Library.GetData<NewTowerData>(name);
        Name   = towerData.dataName;
        Detail = towerData.ToStringEx();
    }

    protected override void Start()
    {
        base.Start();
        attackArea = transform.Find("AttackArea").gameObject;
        // upgradedTower = towerData.UpgradedTower;
        if(!upgradedTower){ menuPresenter.NonUpgrade(); }
    }

    
    public void SetMenuPresenter(FunctionalPanelPresenter presenter) => menuPresenter = presenter;
    public void Upgrade()
    {

    }
    public void Sell(){
        SellingTower.Invoke();
    }

    public override void Select()
    {
        base.Select();
        GetComponent<NewAttacker>().ToSelected();
        attackArea.SetActive(true);
        menuPresenter.ShowTowerMenu();
        menuPresenter.SetTowerPanel(this);       
        menuPresenter.MoveToNewPosition(transform.position, menuPresenter.GetTowerMeunRect(), FunctionalPanelPresenter.Direction.Left);
    }

    public override void Deselect()
    {
        base.Deselect();
        GetComponent<NewAttacker>().ToDeselected();
        attackArea.SetActive(false);
        menuPresenter.HideTowerMenu();
        menuPresenter.UnsetTowerPanel(this);
    }

}
