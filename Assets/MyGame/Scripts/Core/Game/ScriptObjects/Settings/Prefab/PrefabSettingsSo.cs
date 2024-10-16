using UnityEngine;

[CreateAssetMenu(menuName = "TD/Settings/Prefabs/Prefab", fileName = "New Prefab Settings")]
public class PrefabSettingsSo : ScriptableObject
{   
    [Header("-- For Game Manager --")]
    public GameObject GameManager;
    public GameObject GameplayUi;
    public GameObject NewPlayer;
    public GameObject LoadingUIPanel;
    public GameObject BuildManager;
    public GameObject AliveEnemyText;
    public GameObject Thank;
    [Header("-- For Inventory System --")]
    public GameObject InventorySlot;
    [Header("-- For Tower --")]
    public GameObject AttackArea;
}