using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

[CreateAssetMenu(menuName = "TD/Settings/Data/Item", fileName = "New Item Settings")]
public class ItemDataSettings : ScriptableObject, IEnumerable<ItemDataSettings.Settings>
{
    [TableList(ShowIndexLabels = true)]
    public List<Settings> Items;

    public IEnumerator<Settings> GetEnumerator(){
        return Items.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator(){
        return GetEnumerator();
    }

    [Serializable]
    public class Settings
    {   
        [HideLabel]
        [TableColumnWidth(80)]
        public string Name;
        [HideLabel]
        [TableColumnWidth(240)]
        [InlineEditor]
        public NewItemData Data;
    }
}

public enum ItemName
{
    Coin,
    Dude,
    Frog,
    Pink,
    VGuy,
    Warrior
}