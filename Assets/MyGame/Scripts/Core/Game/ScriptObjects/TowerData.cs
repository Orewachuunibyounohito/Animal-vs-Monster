using UnityEngine;

[CreateAssetMenu( fileName = "TowerData", menuName = "TD/Tower Data" )]
public class TowerData : ScriptableObject
{   
    #region Field
    [SerializeField] private string     _description;
    [SerializeField] private int        _cost;
    [SerializeField] private int        _sell;
    [SerializeField] private Sprite     _towerImage;
    [SerializeField] private GameObject _towerPrefab;
    [SerializeField] private WeaponData _weaponData;
    #endregion

    #region Property
    public string     description  => _description;
    public int        cost         => _cost;
    public int        sell         => _sell;
    public Sprite     towerImage   => _towerImage;
    public GameObject towerPrefab  => _towerPrefab;
    public WeaponData weaponData   => _weaponData;
    #endregion
}
