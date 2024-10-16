using System;
using System.Text;
using Sirenix.OdinInspector;
using UnityEngine;

[CreateAssetMenu( fileName = "NewWeapon", menuName = "TD/Weapon Data", order = 3 )]
public class WeaponData : ScriptableObject
{
    private const int PREVIEW_HEIGHT = 67;
    #region Field
    [AssetsOnly]
    [HideLabel, PreviewField(PREVIEW_HEIGHT, ObjectFieldAlignment.Left)]
    [HorizontalGroup("Split"), HorizontalGroup("Split/Left/General Settings/Split", PREVIEW_HEIGHT)]
    [VerticalGroup("Split/Left")]
    [BoxGroup("Split/Left/General Settings")]
    [SerializeField] private GameObject _projectilePrefab;

    [HideLabel, TextArea(4, 14)]
    [HorizontalGroup("Split/Right")]
    [BoxGroup("Split/Right/Description")]
    [SerializeField] private string     _description;

    [BoxGroup("Split/Left/Stats")]
    [SerializeField] private int        _damage;
    [BoxGroup("Split/Left/Stats")]
    [SerializeField] private float      _moveSpeed;
    [BoxGroup("Split/Left/Stats")]
    [SerializeField] private float      _aliveDistance;
    [BoxGroup("Split/Left/Stats")]
    [SerializeField] private float      _attackSpeed;
    [BoxGroup("Split/Left/Stats")]
    [SerializeField] private int        _aoeCapacity;
    [BoxGroup("Split/Left/Stats")]
    [SerializeField] private float      _aoeRadius;
    
    [LabelWidth(55), LabelText("Type")]
    [VerticalGroup("Split/Left/General Settings/Split/Right")]
    [BoxGroup("Split/Left/General Settings")]
    [SerializeField] private WeaponType _weaponType;

    [LabelWidth(55), LabelText("SFX")]
    [VerticalGroup("Split/Left/General Settings/Split/Right")]
    [BoxGroup("Split/Left/General Settings")]
    [SerializeField] private SfxName _sfxName;
    #endregion

    #region Property
    public string     Description      => _description;
    public int        Damage           => _damage;
    public float      MoveSpeed        => _moveSpeed;
    public float      AliveDistance    => _aliveDistance;
    public float      AttackSpeed      => _attackSpeed;
    public int        AoeCapacity      => _aoeCapacity;
    public float      AoeRadius        => _aoeRadius;
    public GameObject ProjectilePrefab{
        get{ 
            return _projectilePrefab? _projectilePrefab : null;
        }
    }
    public WeaponType WeaponType       => _weaponType;
    public string     AnimationName    => _weaponType == WeaponType.Melee? "Attack" : "Idle";
    public SfxName    SfxName          => _sfxName;

    // public void       DataAssignToPrefab()                  => _projectilePrefab?.GetComponent<NewProjectile>().AssignData( this );
    public void       DataAssignToPrefab(){
        if(_projectilePrefab){
            _projectilePrefab.GetComponent<NewProjectile>().AssignData( this );
        }
    }
    // public void       SetTargetToPrefab( Transform target ) => _projectilePrefab?.GetComponent<NewProjectile>().SetTarget( target );
    public void       SetTargetToPrefab( Transform target ){
        if(_projectilePrefab){
            _projectilePrefab.GetComponent<NewProjectile>().SetTarget( target );
        }
    }
    #endregion

    public override string ToString(){
        string result = $"{"Weapon:", -15}{_projectilePrefab.name, 15}\n" +
                        $"{"Damage:", -15}{_damage, 15}\n" +
                        $"{"AS:", -15}{_attackSpeed, 15}\n" +
                        $"{"Targets:", -15}{_aoeCapacity, 15}\n" +
                        $"{"AoE:", -15}{_aoeRadius, 15}";
        return result;
    }

    public string[] ToStringEx(){
        var result = new string[]{
                $"Weapon:\n" +
                $"Damage:\n" +
                $"AS:\n" +
                $"Targets:\n" +
                $"AoE:",

                $"{(_projectilePrefab? _projectilePrefab.name : name)}\n" +
                $"{_damage}\n" +
                $"{_attackSpeed}\n" +
                $"{_aoeCapacity}\n" +
                $"{_aoeRadius}"
        };
        return result;
    }
}
