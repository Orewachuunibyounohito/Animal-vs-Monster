
public class Damageable : AbstractHealth
{
    public virtual void DealDamage( int amount ){
        _hp = System.Math.Clamp( _hp-amount, 0, _maxHp );
    }
}
