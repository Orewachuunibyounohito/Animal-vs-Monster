using UnityEngine;
using UnityEngine.Events;

public class PlayerUIPanel : MonoBehaviour
{
    #region Private Variable
    [SerializeField] private TMPro.TextMeshProUGUI _healthText;
    [SerializeField] private TMPro.TextMeshProUGUI _moneyText;
    [SerializeField] private TMPro.TextMeshProUGUI _nameText;
    #endregion

    #region Const
    private const string HEALTH_TEXT_NAME = "HpText";
    private const string MONEY_TEXT_NAME  = "MoneyText";
    private const string USERNAME_TEXT_NAME  = "Text";
    #endregion

    #region Custom Action
    public UnityAction OnHealthUIUpdate;
    public UnityAction OnMoneyUIUpdate;
    #endregion

    #region Unity Events
    private void Awake(){
        AssignText();
    }
    #endregion

    #region Private Methods
    private void AssignText(){
        TMPro.TextMeshProUGUI[] texts = GetComponentsInChildren<TMPro.TextMeshProUGUI>( true );
        foreach( var text in texts ){
            switch( text.gameObject.name ){
                case HEALTH_TEXT_NAME:
                    _healthText = text;
                    break;
                case MONEY_TEXT_NAME:
                    _moneyText = text;
                    break;
                case USERNAME_TEXT_NAME:
                    _nameText = text;
                    break;
            }
        }
    }
    #endregion

    #region Public Methods
    public void UpdateHealthUI( int health ){
        _healthText.SetText( health.ToString() );
        OnHealthUIUpdate?.Invoke();
    }

    public void UpdateMoneyUI( int money ){
        _moneyText.SetText( money.ToString() );
        OnMoneyUIUpdate?.Invoke();
    }

    public void UpdateNameUI(string name){
        _nameText.SetText(name);
    }

    public void SetVisible( bool visible ){
        GameObject healthRect = _healthText.transform.parent.gameObject;
        GameObject moneyRect = _moneyText.transform.parent.gameObject;
        GameObject nameRect = _nameText.transform.parent.gameObject;
        healthRect.SetActive( visible );
        moneyRect.SetActive( visible );
        nameRect.SetActive(visible);
    }
    #endregion
}
