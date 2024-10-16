using System.Collections.Generic;
using UnityEngine;

public class Helper : MonoBehaviour
{
    #region Private Variable
    [Header( "--Enemy Data--" )]
    [SerializeField] private List<EnemyData>    oldEnemyDatas;
    [SerializeField] private List<NewEnemyData> newEnemyDatas;
    [Header( "--Tower Data--" )] 
    [SerializeField] private List<TowerData>    oldTowerDatas;
    [SerializeField] private List<NewTowerData> newTowerDatas; 
    [Header( "--Item Data--" )]
    [SerializeField] private List<ItemData>     oldItemDatas;
    [SerializeField] private List<NewItemData>  newItemDatas; 
    [Header( "--Wave Data--" )]
    [SerializeField] private List<WaveData>     oldWaveDatas;
    [SerializeField] private List<NewWaveData>  newWaveDatas;
    #endregion

    #region Assign From Old Data
    [ContextMenu( "Assign From Old Data" )]
    public void AllAssignFromOld(){
        for( int idx = 0; idx < oldEnemyDatas.Count && idx < newEnemyDatas.Count; idx++ ){
            newEnemyDatas[idx].AssignFromOldData( oldEnemyDatas[idx] );
        }
        for( int idx = 0; idx < oldTowerDatas.Count && idx < newTowerDatas.Count; idx++ ){
            newTowerDatas[idx].AssignFromOldData( oldTowerDatas[idx] );
        }
        for( int idx = 0; idx < oldItemDatas.Count && idx < newItemDatas.Count; idx++ ){
            newItemDatas[idx].AssignFromOldData( oldItemDatas[idx] );
        }
        for( int idx = 0; idx < oldWaveDatas.Count && idx < newWaveDatas.Count; idx++ ){
            newWaveDatas[idx].AssignFromOldData( oldWaveDatas[idx] );
        }
    }
    [ContextMenu( "Assign Enemy Data From Old Data" )]
    public void AssignEnemy(){
        for( int idx = 0; idx < oldEnemyDatas.Count && idx < newEnemyDatas.Count; idx++ ){
            newEnemyDatas[idx].AssignFromOldData( oldEnemyDatas[idx] );
        }
    }
    [ContextMenu( "Assign Tower Data From Old Data" )]
    public void AssignTower(){
        for( int idx = 0; idx < oldTowerDatas.Count && idx < newTowerDatas.Count; idx++ ){
            newTowerDatas[idx].AssignFromOldData( oldTowerDatas[idx] );
        }
    }
    [ContextMenu( "Assign Item Data From Old Data" )]
    public void AssignItem(){
        for( int idx = 0; idx < oldItemDatas.Count && idx < newItemDatas.Count; idx++ ){
            newItemDatas[idx].AssignFromOldData( oldItemDatas[idx] );
        }
    }
    [ContextMenu( "Assign Wave Data From Old Data" )]
    public void AssignWave(){
        for( int idx = 0; idx < oldWaveDatas.Count && idx < newWaveDatas.Count; idx++ ){
            newWaveDatas[idx].AssignFromOldData( oldWaveDatas[idx] );
        }
    }
    #endregion
}
