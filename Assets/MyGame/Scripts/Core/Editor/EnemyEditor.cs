#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

[CustomEditor( typeof( Enemy ) )]
public class EnemyEditor : Editor
{
    public override void OnInspectorGUI(){
        DrawDefaultInspector();

        Enemy source = (Enemy)target;
        int        damage = 10; 
        
        #region Test Button
        EditorGUILayout.BeginHorizontal();
        damage = EditorGUILayout.IntField( "Damage", damage );
        if( GUILayout.Button( "Attack" ) ){
            source.DealDamage( damage );
            // Debug.Log( $"{source.name} get hurt {damage}, hp: {source.hp}/{source.maxHp}" );
            source.UpdateUI();
        }
        EditorGUILayout.EndHorizontal();

        if( GUILayout.Button( "Fill Up Hp" ) ){
            source.ChangeHp( source.maxHp );
            source.FillUpUI();
        }
        #endregion
    }
}
#endif
