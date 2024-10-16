using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

[CreateAssetMenu(menuName = "TD/Settings/Data/Enemy", fileName = "New Enemy Settings")]
public class EnemyDataSettings : ScriptableObject, IEnumerable<EnemyDataSettings.Settings>
{
    [TableList(ShowIndexLabels = true)]
    public List<Settings> enemySO;

    public IEnumerator<Settings> GetEnumerator(){
        return enemySO.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator(){
        return GetEnumerator();
    }

    [Serializable]
    public class Settings
    {
        [TableColumnWidth(80)]
        public string Name;
        [TableColumnWidth(240)]
        public NewEnemyData Data;
    }
}

public enum EnemyName
{
    Sand,
    Rock,
    SpikedBall,
    Saw,
    SpikeHead,
    BlueBird
}