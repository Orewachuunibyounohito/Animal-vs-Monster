using Sirenix.OdinInspector;
using UnityEngine;

[CreateAssetMenu(menuName = "TD/Settings/Data/Data", fileName = "New Data Settings", order = 0)]
public class DataSettings : ScriptableObject
{
    [TabGroup("Tower Settings")]
    [InlineEditor]
    public TowerDataSettings TowerSettings;
    [TabGroup("Enemy Settings")]
    [InlineEditor]
    public EnemyDataSettings EnemySettings;
    [TabGroup("Item Settings")]
    [InlineEditor]
    public ItemDataSettings  ItemSettings;
}
