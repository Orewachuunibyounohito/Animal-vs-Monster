#if UNITY_EDITOR
using System.Collections.Generic;
using UnityEditor;

// [CustomEditor( typeof( RewardData ) )]
public class RewardDataEditorCustom : Editor
{
    private List<bool> showDetail = new List<bool>();
    private bool       showDetails;

    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        DrawRewardData_New();
    }

    private void DrawRewardData(){
        RewardData rewardData = (RewardData)target;
        if( rewardData.itemData == null ){ return ; }
        
        EditorGUILayout.BeginHorizontal();
        int detailsCount = rewardData.itemData.Count-showDetail.Count;
        showDetails = EditorGUILayout.BeginFoldoutHeaderGroup(  showDetails, "Show Details" );
        EditorGUILayout.IntField( rewardData.itemData.Count );
        EditorGUILayout.EndHorizontal();
        if( showDetails ){
            EditorGUI.indentLevel++;
            if( Selection.activeObject ){
                for( int cnt = detailsCount; cnt > 0; cnt-- ){
                    showDetail.Add( false );
                }
                
                for( int idx = 0; idx < rewardData.itemData.Count; idx++ ){
                    DrawItemDetails( rewardData.itemData[idx], idx );
                }
            }
            EditorGUI.indentLevel--;
        }
        EditorGUILayout.EndFoldoutHeaderGroup();
    }
    private void DrawRewardData_New(){
        RewardData rewardData = (RewardData)target;
        if( rewardData.newItemData == null ){ return ; }
        
        EditorGUILayout.BeginHorizontal();
        int detailsCount = rewardData.newItemData.Count-showDetail.Count;
        showDetails = EditorGUILayout.BeginFoldoutHeaderGroup(showDetails, "Show Details");
        EditorGUILayout.IntField(rewardData.newItemData.Count);
        EditorGUILayout.EndHorizontal();

        if( showDetails ){
            EditorGUI.indentLevel++;
            if( Selection.activeObject ){
                for( int cnt = detailsCount; cnt > 0; cnt-- ){
                    showDetail.Add( false );
                }
                for( int idx = 0; idx < rewardData.newItemData.Count; idx++ ){
                    DrawItemDetails( rewardData.newItemData[idx], idx );
                }
            }
            EditorGUI.indentLevel--;
        }
        EditorGUILayout.EndFoldoutHeaderGroup();
    }

    private void DrawItemDetails( ItemData itemData, int index ){
        if( itemData == null ){ return ; }
        showDetail[index] = EditorGUILayout.Foldout( showDetail[index], $"Elements {index}" );
        if( showDetail[index] ){
            if( Selection.activeObject ){
                itemData.name = itemData.itemName;
                EditorGUILayout.LabelField( "-- Item details --", EditorStyles.boldLabel );

                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.LabelField( "No" );
                EditorGUILayout.IntField( itemData.id );
                EditorGUILayout.EndHorizontal();

                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.LabelField( "Name" );
                EditorGUILayout.TextField( itemData.itemName );
                EditorGUILayout.EndHorizontal();
                
                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.LabelField( "Value" );
                EditorGUILayout.IntField( itemData.value );
                EditorGUILayout.EndHorizontal();

                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.LabelField( "Drop Rare" );
                EditorGUILayout.FloatField( itemData.dropRateRare );
                EditorGUILayout.EndHorizontal();

                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.LabelField( "Count Rare" );
                EditorGUILayout.FloatField( itemData.dropCountRare );
                EditorGUILayout.EndHorizontal();

                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.LabelField( "Stackable" );
                EditorGUILayout.Toggle( itemData.stackable );
                EditorGUILayout.EndHorizontal();

                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.LabelField( "Description" );
                EditorGUILayout.TextField( itemData.description );
                EditorGUILayout.EndHorizontal();
                
                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.LabelField( "Item Type" );
                EditorGUILayout.EnumFlagsField( itemData.itemType );
                EditorGUILayout.EndHorizontal();
            }
        }
    }

    private void DrawItemDetails( NewItemData itemData, int index ){
        if( itemData == null ){ return ; }
        showDetail[index] = EditorGUILayout.Foldout( showDetail[index], $"Elements {index} - {itemData.dataName}" );
        
        if( showDetail[index] ){
            if( Selection.activeObject ){
                EditorGUILayout.LabelField( "-- Item details --", EditorStyles.boldLabel );

                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.LabelField( "No" );
                EditorGUILayout.IntField( itemData.id );
                EditorGUILayout.EndHorizontal();

                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.LabelField( "Name" );
                EditorGUILayout.TextField( itemData.dataName );
                EditorGUILayout.EndHorizontal();
                
                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.LabelField( "Value" );
                EditorGUILayout.IntField( itemData.value );
                EditorGUILayout.EndHorizontal();

                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.LabelField( "Drop Rare" );
                EditorGUILayout.FloatField( itemData.dropRateRare );
                EditorGUILayout.EndHorizontal();

                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.LabelField( "Count Rare" );
                EditorGUILayout.FloatField( itemData.dropAmountRare );
                EditorGUILayout.EndHorizontal();

                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.LabelField( "Stackable" );
                EditorGUILayout.Toggle( itemData.stackable );
                EditorGUILayout.EndHorizontal();

                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.LabelField( "Description" );
                EditorGUILayout.TextField( itemData.description );
                EditorGUILayout.EndHorizontal();
                
                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.LabelField( "Item Type" );
                EditorGUILayout.EnumFlagsField( itemData.itemType );
                EditorGUILayout.EndHorizontal();
            }
        }
    }
}
#endif
