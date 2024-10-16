using UnityEngine;

public abstract class AbstractHealth : MonoBehaviour
{
    #region Field
    [SerializeField] protected int   _hp;
    [SerializeField] protected int   _maxHp;
    [SerializeField] protected HpBar _hpBar;
    #endregion

    #region Property
    public int hp    => _hp;
    public int maxHp => _maxHp;
    #endregion

    protected virtual void Awake(){
        _hp    = _maxHp;
        _hpBar = GetComponentInChildren<HpBar>();
    }

    #region Change Hp
    public virtual void ChangeHp( int amount ){
        _hp = System.Math.Clamp( _hp+amount, 0, _maxHp );
    }
    #endregion

    #region Update UI
    public void UpdateUI(){
        _hpBar.UpdateUI( _hp/(float)maxHp );
    }
    #endregion

    #region Test Function
    public void FillUpUI(){
        _hpBar.FillUpUI();
    }
    #endregion

}
