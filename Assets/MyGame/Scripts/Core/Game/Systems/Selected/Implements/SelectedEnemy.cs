using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectedEnemy : SelectableComponenet, IInfoData
{
    public string   Name { get; private set; }
    public string[] Detail { get; private set; }

    void Awake(){
        var enemyData = GameManager.Instance.Library.GetData<NewEnemyData>(name);
        Name   = enemyData.dataName;
        Detail = enemyData.ToStringEx();
    }
}
