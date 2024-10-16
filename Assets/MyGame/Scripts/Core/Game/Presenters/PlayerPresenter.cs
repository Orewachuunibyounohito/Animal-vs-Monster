using UnityEngine;

public class PlayerPresenter : MonoBehaviour
{
    #region Private Variable
    [SerializeField] private NewPlayer     model; 
    [SerializeField] private PlayerUIPanel playerUIPanel;
    [SerializeField] private PlayerAudio   playerAudio;
    #endregion

    #region Unity Events
    private void Awake(){
        model.OnHealthChanged += playerUIPanel.UpdateHealthUI;
        model.OnTakeDamage    += playerAudio.PlayHurtAudio;
        model.OnMoneyChanged  += playerUIPanel.UpdateMoneyUI;
        model.NameChanged     += playerUIPanel.UpdateNameUI;
    }
    #endregion
}

