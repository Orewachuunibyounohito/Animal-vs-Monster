#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

[CustomEditor( typeof( Weapon ) )]
public class WeaponEditorCustom : Editor
{
    public override void OnInspectorGUI(){
        DrawDefaultInspector();

        Weapon weapon = (Weapon)target;
        if( GUILayout.Button( "Throw projectile" ) ){
            weapon.DoAttack();
        }
    }
}
#endif
