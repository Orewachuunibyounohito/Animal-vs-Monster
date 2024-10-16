using System;
using Sirenix.OdinInspector;
using TD.Item;
using UnityEngine;
using UnityEngine.UI;

public class SelectedItem : SelectableComponenet, IInfoData
{
    public string   Name { get; private set; }
    public string[] Detail { get; private set; }

    private int       value;
    private NewPlayer owner;

    [ShowInInspector]
    private InventorySystem.InventorySlot slot;
    private FunctionalPanelPresenter      menuPresenter;

    public event Action<int> SellingItem;

    protected override void Start(){
        var itemData = GameManager.Instance.Library.GetData<NewItemData>(slot.Item.Name);
        Name   = itemData.dataName;
        Detail = itemData.ToStringEx();
        value  = itemData.value;
        base.Start();
        infoData = this;
        owner = GameManager.Instance.newPlayer;
    }

    public void SetSlot(InventorySystem.InventorySlot slot)          => this.slot = slot;
    public void SetMenuPresenter(FunctionalPanelPresenter presenter) => menuPresenter = presenter;
    public void Use()                                                => slot.Item.Use(owner);
    public void Sell(){
        owner.Inventory.RemoveItem(slot, 1);

        var sellingPrice = (int)Math.Floor(value * 0.2f);
        SellingItem.Invoke(sellingPrice);
    }

    public override void Select()
    {
        base.Select();
        menuPresenter.ShowItemMenu();
        menuPresenter.SetItemPanel(this);

        var slotRect  = transform.parent.parent.GetComponent<RectTransform>().rect;
        var newPosition = transform.position;
        menuPresenter.MoveToNewPosition(newPosition, slotRect, FunctionalPanelPresenter.Direction.Down);
    }

    public override void Deselect()
    {
        base.Deselect();
        menuPresenter.HideItemMenu();
        menuPresenter.UnsetItemPanel(this);
    }
}
