using System;
using Sirenix.OdinInspector;
using UnityEngine;

[CreateAssetMenu( fileName = "TowerData", menuName = "TD/New Tower Data" )]
public class NewTowerData : InstantiateData
{   
    #region Field
    [HideLabel, TextArea(4, 14)]
    [VerticalGroup(SPLIT)]
    [BoxGroup(SPLIT + "/Description")]
    [SerializeField] private string     _description;

    [HorizontalGroup(SPLIT + "/Stats/Split", LabelWidth = 60)]
    [VerticalGroup(SPLIT + "/Stats/Split/Middle")]
    [BoxGroup(SPLIT + "/Stats")]
    [SerializeField] private int        _cost;
    
    [VerticalGroup(SPLIT + "/Stats/Split/Middle")]
    [BoxGroup(SPLIT + "/Stats")]
    [SerializeField] private int        _sell;

    [Title("Weapon Data", TitleAlignment = TitleAlignments.Centered)]
    [InlineEditor]
    [HideLabel]
    [SerializeField] private WeaponData _weaponData;

    // public WeaponDetail detail;
    #endregion

    #region Property
    public string     description  => _description;
    public int        cost         => _cost;
    public int        sell         => _sell;
    public WeaponData weaponData   => _weaponData;
    #endregion
    
    #region Help Function
    public void AssignFromOldData( TowerData data ){
        _prefab      = data.towerPrefab;
        _cost        = data.cost;
        _sell        = data.sell;
        _description = data.description;
        _weaponData  = data.weaponData;
    }
    #endregion
    
    public override string ToString(){
        return $"{"Cost:", -15}{_cost, 15}\n" +
               $"{"Sell:", -15}{_sell, 15}\n" +
               $"{_weaponData}";
    }

    public string[] ToStringEx(){
        var weaponDetail = _weaponData.ToStringEx();
        var result       = new string[]{
            $"Cost:\n" +
            $"Sell:\n" +
            $"{weaponDetail[0]}",

            $"{_cost}\n" +
            $"{_sell}\n" +
            $"{weaponDetail[1]}"
        };
        
        return result;
    }
}

[Serializable]
public class WeaponDetail
{   
    private const int PREVIEW_HEIGHT = 67;

    [AssetsOnly, ReadOnly]
    [HideLabel, PreviewField(PREVIEW_HEIGHT, ObjectFieldAlignment.Left)]
    [HorizontalGroup("Split"), HorizontalGroup("Split/Left/General Settings/Split", PREVIEW_HEIGHT)]
    [VerticalGroup("Split/Left")]
    [BoxGroup("Split/Left/General Settings")]
    public GameObject ProjectilePrefab;

    [ReadOnly, LabelWidth(55), LabelText("Type")]
    [VerticalGroup("Split/Left/General Settings/Split/Right")]
    [BoxGroup("Split/Left/General Settings")]
    public string Name;

    [ReadOnly, LabelWidth(55), LabelText("Type")]
    [VerticalGroup("Split/Left/General Settings/Split/Right")]
    [BoxGroup("Split/Left/General Settings")]
    public WeaponType WeaponType;


    [ReadOnly, HideLabel, TextArea(4, 14)]
    [HorizontalGroup("Split/Right")]
    [BoxGroup("Split/Right/Description")]
    public string     Description;

    [ReadOnly]
    [BoxGroup("Split/Left/Stats")]
    public int        Damage;
    [ReadOnly]
    [BoxGroup("Split/Left/Stats")]
    public float      MoveSpeed;
    [ReadOnly]
    [BoxGroup("Split/Left/Stats")]
    public float      AliveDistance;
    [ReadOnly]
    [BoxGroup("Split/Left/Stats")]
    public float      AttackSpeed;
    [ReadOnly]
    [BoxGroup("Split/Left/Stats")]
    public int        AoeCapacity;
    [ReadOnly]
    [BoxGroup("Split/Left/Stats")]
    public float      AoeRadius;

    public WeaponDetail(WeaponData data){
        Name          = data.name;
        WeaponType    = data.WeaponType;
        Damage        = data.Damage;
        AttackSpeed   = data.AttackSpeed;
        AliveDistance = data.AliveDistance;
        AttackSpeed   = data.AttackSpeed;
        AoeCapacity   = data.AoeCapacity;
        AoeRadius     = data.AoeRadius;
    }
}
