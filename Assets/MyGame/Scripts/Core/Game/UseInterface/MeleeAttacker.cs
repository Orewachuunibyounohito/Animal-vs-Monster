using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[RequireComponent( typeof( MeleeWeapon ) )]
public class MeleeAttacker : NewAttacker
{
    #region Private Variable
    [SerializeField] protected List<List<Vector2>> _attackPaths = new List<List<Vector2>>();
    #endregion

    protected override void Awake()
    {
        base.Awake();
        AttackPathAssign();
    }

    private void OnEnable(){
        // GetComponentInChildren<HitBox>(true).OnHitEnemy += (target) => target.DealDamage(_weapon.damage);
        GetComponentInChildren<HitBox>(true).OnHitEnemy += HitEnemy;
    }

    protected override void FixedUpdate()
    {
        base.FixedUpdate();
        if( _target == default ){ ReturnDefaultRotation(); }
    }

    private void OnDisable(){
        // GetComponentInChildren<HitBox>(true).OnHitEnemy -= (target) => target.DealDamage(_weapon.damage);
        GetComponentInChildren<HitBox>(true).OnHitEnemy -= HitEnemy;
    }

    #region Melee Attack

    #region State
    public void ToAttackState(){
        state = State.Attacking;
    }
    public void AttackExit(){
        state = State.Lock;
    }
    #endregion

    #region Attack Path Assign
    protected virtual void AttackPathAssign(){
        PolygonCollider2D polygon = transform.GetChild(0).GetComponent<PolygonCollider2D>();
        for( int idx = 0; idx < polygon.pathCount; idx++ ){
            _attackPaths.Add( polygon.GetPath( idx ).ToList() );
        }
        GetComponent<PolygonCollider2D>().SetPath( 0, _attackPaths[0] );
    }
    #endregion

    #region Attack Path Changed
    public void AttackPathChanged( int pathIndex ){
        GetComponent<PolygonCollider2D>().SetPath( 0, _attackPaths[pathIndex] );
    }
    #endregion

    #endregion

    #region Simple Look At
    protected override void SimpleLookAt(){
        if( _inCoolDown ){ return ; }
        transform.rotation = Quaternion.AngleAxis( Vector2.SignedAngle( Vector2.right, _target.transform.position-transform.position ), Vector3.forward );

    }
    protected virtual void ReturnDefaultRotation(){
        if( _inCoolDown ){ return ; }
        if( transform.rotation.x <= 90 && transform.rotation.x >= -90 ){ transform.rotation = Quaternion.Euler( 0f, 0f, 0f ); }
        else{ transform.rotation = Quaternion.Euler( 0f, 180f, 0f ); }
    }
    #endregion
    
    private void HitEnemy(NewEnemy target){ target.DealDamage(_weapon.damage); }
}
