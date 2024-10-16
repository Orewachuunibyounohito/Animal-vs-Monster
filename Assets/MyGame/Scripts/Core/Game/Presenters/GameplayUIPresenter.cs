using UnityEngine;

public class GameplayUIPresenter : MonoBehaviour
{
    #region Private Variable
    [SerializeField] private GameManager       _gameManager;
    [SerializeField] private GameplayUIPanel   _panel;
    [SerializeField] private GameplayUIAnimate _animate;
    #endregion

    #region Unity Events
    private void Awake(){
    }
    private void Start(){
        UIBinding();
    }
    #endregion

    #region Public Methods
    public void UIBinding(){
        _gameManager = GetComponent<GameManager>();
        _panel       = _gameManager.gameplayUI;
        _animate = new GameplayUIAnimate(_panel.GetInfoPanel().GetComponent<Animator>());
        
        _panel.OnWorldMapClick.AddListener( _gameManager.EnterWorldMap );
        _panel.OnBattleStartClick.AddListener( _gameManager.NextWave );
        _panel.OnBattleStartClick.AddListener( _panel.HideBattleButton );
        _panel.BarFoldClicked.AddListener( _panel.TowerBarSwitch );
        _panel.SaveDataClicked.AddListener(_panel.OnSaveClicked);
        _panel.InventoryClicked.AddListener(_panel.OnInventoryClicked);
        _panel.InventoryClosed.AddListener(_panel.OnInventoryClosed);
        _panel.RetryClicked.AddListener(_gameManager.OnStageRetry);
        _panel.BackToWorldMapClicked.AddListener(_gameManager.OnGameOverBack);
        _panel.GetInfoPanel().OnShowInfo += _animate.InfoPanelExtend;
        _panel.GetInfoPanel().OnHideInfo += _animate.InfoPanelFold;

        _gameManager.OnTimeChanged.AddListener( _panel.UpdateTimerUI );
        _gameManager.OnWaveChanged.AddListener( _panel.UpdateWaveUI );
        _gameManager.OnStageClear.AddListener( _panel.ShowWorldMapButton );

        _gameManager.newPlayer.TowerBarChanged += _panel.TowerBarAutoResize;
    }
    #endregion

}
