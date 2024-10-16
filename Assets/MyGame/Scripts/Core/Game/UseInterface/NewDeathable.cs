using UnityEngine.Events;

public abstract class NewDeathable : NewDamageable, IDeathable
{
    #region Unity Event
    public UnityEvent CreatureDied;
    #endregion

    public override void ChangeHp(int amount)
    {
        if( _currentHeath == 0 ){ return ; }
        base.ChangeHp(amount);
        if( _currentHeath == 0 ){
            Die();
        }
    }

    public void Die(){
        CreatureDied.Invoke();
    }
}
