using System;
using UnityEngine;
using UnityEngine.Events;

public class NewWeapon : MonoBehaviour
{   
    #region Field
    [SerializeField] protected WeaponData _weaponData;
    #endregion

    #region Property
    public virtual NewAttacker   owner       => GetComponent<NewAttacker>();
    public int                   damage      => _weaponData.Damage;
    public float                 attackRange => _weaponData.AliveDistance;
    public float                 attackSpeed => _weaponData.AttackSpeed;
    public WeaponType            type        => _weaponData.WeaponType;
    public SfxName               SfxName     => _weaponData.SfxName;
    #endregion

    #region Const
    private const float  PROJECTILE_SPWAN_OFFSET  = 0.5f;
    #endregion

    #region Event
    public UnityEvent OnAttack;
    #endregion

    protected virtual void Awake(){
    }

    #region Do Attack
    public virtual void DoAttack( Transform target ){
        Vector2 direction = ( target.position-transform.position ).normalized;
        _weaponData.SetTargetToPrefab( target );
        Instantiate( _weaponData.ProjectilePrefab, (Vector2)transform.position+direction*PROJECTILE_SPWAN_OFFSET, _weaponData.ProjectilePrefab.transform.rotation );
        
        OnAttack.Invoke();
    }

    #endregion

    #region Public Methods
    public void SetWeaponData( WeaponData weaponData ){
        _weaponData = weaponData;
        _weaponData.DataAssignToPrefab();
    }
    #endregion
}
