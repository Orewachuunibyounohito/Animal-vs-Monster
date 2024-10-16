using UnityEngine;

public class MeleeWeapon : NewWeapon
{   
    #region Property
    public override NewAttacker owner => GetComponent<MeleeAttacker>();
    #endregion

    protected override void Awake(){}

    #region Do Attack
    public override void DoAttack( Transform target ){     
        owner.GetComponent<Animator>().speed = _weaponData.AttackSpeed;
        owner.ToAttack();
        
        OnAttack.Invoke();
    }
    #endregion
    
}
