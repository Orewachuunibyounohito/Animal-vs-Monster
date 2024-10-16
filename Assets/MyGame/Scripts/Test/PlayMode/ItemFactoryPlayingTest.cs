using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;

public class ItemFactoryPlayingTest
{
    [Category("TD/Item")]
    [Test]
    public void NewHealthPotionThenMatchData(){
        new GameObject("GameManager").AddComponent<LibraryManager>().gameObject
                                     .AddComponent<GameManager>();
        var itemData = GameManager.Instance.Library.GetData<NewItemData>("HealthPotion");
        var item     = ItemFactory.GenerateItem("HealthPotion");

        var actual = item.Name == itemData.dataName && item.Capacity == itemData.Capacity && item.Icon == itemData.image;

        Assert.IsTrue(actual);
    }

    [Category("TD/Item")]
    [Test]
    public void PlayerGetAHealthPotion(){
        var prefabSettings = Resources.Load<PrefabSettingsSo>("TD/Prefabs/Prefab Settings");
        var gameManager    = Object.Instantiate(prefabSettings.GameManager).GetComponent<GameManager>()
                                                                                 .InstantiatePlayer_Test();
        var item     = ItemFactory.GenerateItem("HealthPotion");
        // var amount   = 1;
        var inventorySystem = new InventorySystem(gameManager);
        inventorySystem.AddItem(gameManager.newPlayer.Inventory, item);

        var actual = gameManager.newPlayer.Inventory.Slots.Find((slot) => slot.Item.Name == item.Name);
        var isSameNameAndCount = actual.Item.Name == item.Name && actual.Amount == 1;

        Assert.IsTrue(isSameNameAndCount);
    }

    [Category("TD/Item")]
    [Test]
    public void PlayerGet10HealthPotion(){
        var prefabSettings = Resources.Load<PrefabSettingsSo>("TD/Prefabs/Prefab Settings");
        var gameManager    = Object.Instantiate(prefabSettings.GameManager).GetComponent<GameManager>()
                                                                                 .InstantiatePlayer_Test();
        var item     = ItemFactory.GenerateItem("HealthPotion");
        var amount   = 10;
        var inventorySystem = new InventorySystem(gameManager);
        inventorySystem.AddItem(gameManager.newPlayer.Inventory, item, amount);

        var actual = gameManager.newPlayer.Inventory.Slots.Find((slot) => slot.Item.Name == item.Name);
        var isSameNameAndCount = actual.Item.Name == item.Name && actual.Amount == 10;

        Assert.IsTrue(isSameNameAndCount);
    }

    [Category("TD/Item")]
    [Test]
    public void PlayerGet200HealthPotion(){
        var prefabSettings = Resources.Load<PrefabSettingsSo>("TD/Prefabs/Prefab Settings");
        var gameManager    = Object.Instantiate(prefabSettings.GameManager).GetComponent<GameManager>()
                                                                                 .InstantiatePlayer_Test();
        var item     = ItemFactory.GenerateItem("HealthPotion");
        var amount   = 200;
        var inventorySystem = new InventorySystem(gameManager);
        inventorySystem.AddItem(gameManager.newPlayer.Inventory, item, amount);

        var actual = gameManager.newPlayer.Inventory.GetAmountByItemName(item.Name);
        var expected = 200;

        Assert.AreEqual(expected, actual);
    }

    [Category("TD/Item")]
    [Test]
    public void PlayerGetHealthPotionOverCapacity(){
        var prefabSettings = Resources.Load<PrefabSettingsSo>("TD/Prefabs/Prefab Settings");
        var gameManager    = Object.Instantiate(prefabSettings.GameManager).GetComponent<GameManager>()
                                                                                 .InstantiatePlayer_Test();
        var item     = ItemFactory.GenerateItem("HealthPotion");
        var amount   = item.Capacity + 1;
        var inventorySystem = new InventorySystem(gameManager);
        inventorySystem.AddItem(gameManager.newPlayer.Inventory, item, (int)amount);

        var actual = gameManager.newPlayer.Inventory.GetSlotsByItemName(item.Name).Count;
        var expected = 2;

        Assert.AreEqual(expected, actual);
    }

    [Category("TD/Item")]
    [Test]
    public void PlayerGet300HealthPotionThenOverviewMatchItemMap(){
        var prefabSettings = Resources.Load<PrefabSettingsSo>("TD/Prefabs/Prefab Settings");
        var gameManager    = Object.Instantiate(prefabSettings.GameManager).GetComponent<GameManager>()
                                                                                 .InstantiatePlayer_Test();
        var item     = ItemFactory.GenerateItem("HealthPotion");
        var amount   = item.Capacity + 1;
        var inventorySystem = new InventorySystem(gameManager);
        var inventory = gameManager.newPlayer.Inventory;
        inventorySystem.AddItem(inventory, item, (int)amount);
        var slots = gameManager.newPlayer.Inventory.GetSlotsByItemName(item.Name);

        var actual = 0;
        foreach(var slot in slots){
            actual += slot.Amount;
        }
        var expected = inventory.GetAmountByItemName(item.Name);

        Assert.AreEqual(expected, actual);
    }
}
