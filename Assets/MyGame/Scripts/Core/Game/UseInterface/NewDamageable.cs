using UnityEngine;
using UnityEngine.Events;

public abstract class NewDamageable : MonoBehaviour, IDamageable
{
    #region Protected Variable
    [Min( 1 )]
    [SerializeField] protected int _maxHealth;

    protected int _currentHeath;
    #endregion

    #region Event
    public UnityAction<int> OnHealthChanged;
    public UnityAction      OnTakeDamage;
    #endregion

    protected virtual void Awake(){
        _currentHeath = _maxHealth;
    }

    #region IDamageable
    public virtual void DealDamage( int damage ){
        if(damage <= 0) return ;
        ChangeHp( -damage );
        OnTakeDamage?.Invoke();
    }
    public virtual void ChangeHp( int amount ){
        _currentHeath = Mathf.Clamp( _currentHeath+amount, 0, _maxHealth );
        OnHealthChanged?.Invoke( _currentHeath );
    }
    #endregion

}
