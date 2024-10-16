#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

// [CustomEditor( typeof( SpawnEnemy ) )]
public class SpawnEnemyCustom : Editor
{   
    /*
    SerializedProperty myBool;

    bool openDefault;

    private void OnEnable(){
        myBool = serializedObject.FindProperty( nameof( SpawnEnemy.inspectorTest ) );
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        SpawnEnemy spawnEnemy = (SpawnEnemy)target;
        
        EditorGUILayout.BeginHorizontal();
        myBool.boolValue = EditorGUILayout.Toggle( myBool.displayName, myBool.boolValue );
        // openDefault = EditorGUILayout.Toggle( myBool.displayName, openDefault );
        spawnEnemy.inspectorTest = myBool.boolValue;
        // spawnEnemy.inspectorTest = openDefault;
        EditorGUILayout.EndHorizontal();

        if( GUILayout.Button( "Invoke") ){ 
            spawnEnemy.inspectorTest = !spawnEnemy.inspectorTest;
        }

        if( spawnEnemy.inspectorTest ){
            DrawDefaultInspector();
        }
        // serializedObject.ApplyModifiedProperties();
        
    }
    */
}
#endif
