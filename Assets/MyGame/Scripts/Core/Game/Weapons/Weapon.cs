using UnityEngine;
using UnityEngine.Events;

public class Weapon : MonoBehaviour
{   
    #region Field
    [SerializeField] private WeaponData _weaponData;
    #endregion

    #region Property
    public Attacker   owner  => GetComponent<Attacker>();
    public int        damage => _weaponData.Damage;
    public WeaponType type   => _weaponData.WeaponType;

    public NewAttacker newOwner => GetComponent<NewAttacker>();
    #endregion

    #region Const
    private const string MELEE_ATTACK_SELECTED_ANIMATION = "Attack_Selected";
    private const string MELEE_ATTACK_ANIMATION          = "Attack";
    private const float  PROJECTILE_SPWAN_OFFSET         = 0.5f;
    #endregion

    #region Event
    public UnityEvent OnAttack;
    #endregion

    private void Awake(){
        switch( _weaponData.WeaponType ){
            case WeaponType.Ranged:
                _weaponData.DataAssignToPrefab();
                break;
        }
    }

    #region Do Attack
    public void DoAttack(){
        switch( _weaponData.WeaponType ){
            case WeaponType.Ranged:
                Instantiate( _weaponData.ProjectilePrefab, transform.position, _weaponData.ProjectilePrefab.transform.rotation );
                break;
            case WeaponType.Melee:
                Attacker attacker = GetComponent<Attacker>();
                if( attacker.IsSelectedState() ){
                    attacker.ChangeAnimationState( MELEE_ATTACK_SELECTED_ANIMATION );
                }else{
                    attacker.ChangeAnimationState( MELEE_ATTACK_ANIMATION );
                }
                attacker.GetComponent<Animator>().speed = _weaponData.AttackSpeed;
                attacker.AnimationDo();
                break;
        }
        OnAttack.Invoke();
    }
    public void DoAttack( Transform target ){
        Vector2 direction = ( target.position-transform.position ).normalized;
        switch( _weaponData.WeaponType ){
            case WeaponType.Ranged:
                _weaponData.SetTargetToPrefab( target );
                Instantiate( _weaponData.ProjectilePrefab, (Vector2)transform.position+direction*PROJECTILE_SPWAN_OFFSET, _weaponData.ProjectilePrefab.transform.rotation );
                break;
            case WeaponType.Melee:
                MeleeAttacker melee = (MeleeAttacker)newOwner;
                melee.GetComponent<Animator>().speed = _weaponData.AttackSpeed;
                melee.ToAttack();
                break;
        }
        OnAttack.Invoke();
    }
    #endregion

    #region Test Function
    // private void TestAttack(){
    //     if( Input.GetKeyUp( KeyCode.Space ) ){
    //         DoAttack();
    //     }
    // }
    #endregion
    
}
