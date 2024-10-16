using System;
using System.Collections.Generic;
using TD.Item;
using TMPro;
using UnityEngine;
using Object = UnityEngine.Object;

public class InventorySystem
{
    private GameManager gameManager;
    private GameObject  inventorySlotPrefab;
    private Dictionary<InventorySlot, InventorySlotPanel> slotsBinding;

    public InventorySystem(GameManager gameManager){
        this.gameManager    = gameManager;
        inventorySlotPrefab = PrefabRepository.InventorySlot;
        slotsBinding        = new Dictionary<InventorySlot, InventorySlotPanel>();
    }

    public void AddItem(Inventory inventory, Item item, int amount = 1){
        var newSlots = inventory.AddItem(item, amount);
        
        foreach(var slot in newSlots){
            var slotPanel = gameManager.gameplayUI.AddItemIntoInventory(inventorySlotPrefab, slot)
                                                  .GetComponent<InventorySlotPanel>();
            slotPanel.AmountText = slotPanel.transform.Find("Mask/Amount").GetComponent<TextMeshProUGUI>();
            slotsBinding.Add(slot, slotPanel);
        }
    }

    // public void RemoveItem(Inventory inventory, Item item, int amount = 1){
    //     inventory
    // }

    public void RemoveSlot(InventorySlot slot){
        var slotInWorld = slotsBinding[slot].gameObject;
        slotsBinding.Remove(slot);
        Object.Destroy(slotInWorld);
    }

    public void Test_AddItem(Inventory inventory){
        var item = ItemFactory.GenerateItem("FrozenPotion");
        AddItem(inventory, item, 50);
    }

    public void UpdateInventorySlotAmount(InventorySlot slot, int amount){
        if(amount == 0){ return ; }
        slotsBinding[slot].AmountText.text = amount.ToString();
    }

    public class Inventory
    {
        private Dictionary<string, int>                 overview;
        private Dictionary<string, List<InventorySlot>> itemMap;

        public List<InventorySlot> Slots;

        public event Action<InventorySlot, int> AmountChanged;
        public event Action<InventorySlot>      ItemUseUp;

        public Inventory(){
            overview = new Dictionary<string, int>();
            itemMap  = new Dictionary<string, List<InventorySlot>>();
            Slots    = new List<InventorySlot>();
        }

        public List<InventorySlot> AddItem(Item item, int amount){
            if(overview.ContainsKey(item.Name)){
                overview[item.Name] += amount;
            }else{
                overview.Add(item.Name, amount);
                itemMap.Add(item.Name, new List<InventorySlot>());
            }

            var remainingAmount = amount;
            InventorySlot slot;
            int pickAmount;

            if(itemMap.TryGetValue(item.Name, out List<InventorySlot> slots)){
                var slotEnumerator = slots.GetEnumerator();
                while(remainingAmount > 0 && slotEnumerator.MoveNext()){
                    slot             = slotEnumerator.Current;
                    pickAmount       = Math.Min((int)item.Capacity - slot.Amount, remainingAmount);
                    slot.Amount      += pickAmount;
                    remainingAmount -= pickAmount;
                    AmountChanged?.Invoke(slot, slot.Amount);
                }
            }

            var result = new List<InventorySlot>();
            while(remainingAmount > 0){
                pickAmount       = Math.Min((int)item.Capacity, remainingAmount);
                slot             = new InventorySlot(item, pickAmount);
                remainingAmount -= pickAmount;
                
                slot.Item.Used += delegate{ OnItemUsed(slot); };
                slot.UseUp     += delegate{ OnItemUseUp(slot); };
                itemMap[item.Name].Add(slot);
                Slots.Add(slot);
                result.Add(slot);
            }
            Slots.Sort();
            return result;
        }

        public void RemoveItem(InventorySlot slot, int amount){
            slot.Amount -= amount;
            if(slot.Amount == 0){
                Slots.Remove(slot);
                itemMap[slot.Item.Name].Remove(slot);
            }
            if(itemMap[slot.Item.Name].Count == 0){ itemMap.Remove(slot.Item.Name); }

            overview[slot.Item.Name] -= amount;
            if(overview[slot.Item.Name] == 0){
                overview.Remove(slot.Item.Name);
                ItemUseUp.Invoke(slot);
            }

            AmountChanged?.Invoke(slot, slot.Amount);
        }

        public void OnItemUsed(InventorySlot slot){
            overview[slot.Item.Name]--;
            if(overview[slot.Item.Name] == 0){ overview.Remove(slot.Item.Name); }
            AmountChanged?.Invoke(slot, slot.Amount);
        }
        public void OnItemUseUp(InventorySlot slot){
            itemMap[slot.Item.Name].Remove(slot);
            if(itemMap[slot.Item.Name].Count == 0){ itemMap.Remove(slot.Item.Name); }
            Slots.Remove(slot);
            ItemUseUp.Invoke(slot);
            slot.UseUp -= delegate{ OnItemUseUp(slot); };
        }
        public void OnItemSold(InventorySlot slot, int amount){
            overview[slot.Item.Name] -= amount;
            if(overview[slot.Item.Name] == 0){ overview.Remove(slot.Item.Name); }
            AmountChanged?.Invoke(slot, slot.Amount);
        }

        public int GetAmountByItemName(string itemName) => overview[itemName];
        public int GetSlotCountByItemName(string itemName) => itemMap[itemName].Count;
        public List<InventorySlot> GetSlotsByItemName(string itemName) => itemMap[itemName];
    }

    [Serializable]
    public class InventorySlot : IComparable<InventorySlot>
    {
        public Item Item;
        public int  Amount;

        public event Action UseUp;

        public InventorySlot(){}
        public InventorySlot(Item item, int amount){
            Item   = item ;
            Amount = amount;
            Item.Used += OnItemUsed;
        }

        public void OnItemUsed(){
            Amount--;
            if(Amount == 0){
                UseUp.Invoke();
                Item.Used -= OnItemUsed;
            }
        }

        public void OnItemSold(int amount){
            Amount -= amount;
            if(Amount == 0){
                UseUp.Invoke();
                Item.Used -= OnItemUsed;
            }
        }

        public int CompareTo(InventorySlot other)
        {
            int result = Item.CompareTo(other.Item);
            if (result == 0){ result = other.Amount - Amount; }
            return result;
        }
    }

}
