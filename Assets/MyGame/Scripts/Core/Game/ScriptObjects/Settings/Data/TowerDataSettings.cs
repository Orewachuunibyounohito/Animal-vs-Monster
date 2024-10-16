using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

[CreateAssetMenu(menuName = "TD/Settings/Data/Tower", fileName = "New Tower Settings")]
public class TowerDataSettings : ScriptableObject, IEnumerable<TowerDataSettings.Settings>{
    [TableList(ShowIndexLabels = true)]
    public List<Settings> Towers;

    public IEnumerator<Settings> GetEnumerator(){
        return Towers.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator(){
        return GetEnumerator();
    }

    [Serializable]
    public class Settings
    {    
        [HideLabel]
        [TableColumnWidth(40)]
        public string Name;

        [HideLabel]
        [TableColumnWidth(200)]
        [InlineEditor]
        public NewTowerData Data;
    }
}

public enum TowerName
{
    Dude,
    Frog,
    Pink,
    VGuy,
    Warrior
}