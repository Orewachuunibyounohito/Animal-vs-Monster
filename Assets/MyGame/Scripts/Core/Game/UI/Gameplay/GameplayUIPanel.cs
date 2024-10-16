using System;
using TD.Info;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class GameplayUIPanel : MonoBehaviour
{
    #region Private Variable
    [SerializeField] private TMP_Text   _timerText;
    [SerializeField] private TMP_Text   _waveText;
    [SerializeField] private GameObject _clear, _mall, _detail, _towerBar, _infoPanel, _inventoryPanel, _functionalPanel, _gameOverPanel;
    [SerializeField] private Button     _worldMap, _battleStart, _barFold, _saveData, _inventoryIcon;

    private RectTransform _inventoryContent;

    private bool _towerBarIsFold = false;
    #endregion

    #region Property
    public UnityEvent OnWorldMapClick    => _worldMap.onClick;
    public UnityEvent OnBattleStartClick => _battleStart.onClick;
    public UnityEvent BarFoldClicked     => _barFold.onClick;
    public UnityEvent SaveDataClicked    => _saveData.onClick;
    public UnityEvent InventoryClicked   => _inventoryIcon.onClick;
    public UnityEvent InventoryClosed    => _inventoryPanel.GetComponent<InventoryPanel>().CloseButton.onClick;
    public UnityEvent RetryClicked       => _gameOverPanel.GetComponent<GameOverPanel>().Retry.onClick;
    public UnityEvent BackToWorldMapClicked => _gameOverPanel.GetComponent<GameOverPanel>().BackToWorldMap.onClick;
    #endregion

    private void Awake(){
        var canvas = GetComponent<Canvas>();
        canvas.renderMode = RenderMode.ScreenSpaceCamera;
        canvas.worldCamera = Camera.main;

        _inventoryIcon    = transform.Find("InventoryIcon").GetComponent<Button>();
        _inventoryPanel   = transform.Find("InventoryPanel").gameObject;
        _inventoryContent = _inventoryPanel.GetComponentInChildren<ScrollRect>().content;

        _functionalPanel = transform.Find("FunctionalPanel").gameObject;
        _gameOverPanel   = transform.Find("GameOverPanel").gameObject;
    }

    private void OnEnable(){
        TowerBarAutoResize();
    }
    
    #region Private Methods
    private void Init(){
        _clear.SetActive(false);
        _mall.SetActive(false);
        _detail.SetActive(false);
        _worldMap.transform.parent.gameObject.SetActive(false);
        _battleStart.gameObject.SetActive(true);
        _inventoryPanel.SetActive(false);
        _inventoryIcon.gameObject.SetActive(true);
        _gameOverPanel.SetActive(false);

        _infoPanel.transform.localScale = new Vector3(0, 1, 1);

        TowerBarInit();

        UpdateTimerUI( 0 );
        UpdateWaveUI( "0/0" );
    }
    #endregion

    #region Public Methods
    public void UpdateTimerUI( float time ){
        var text = time.ToString( "0.0" );
        _timerText.SetText( $"{text}" );
    }
    public void UpdateWaveUI( string waveText ){
        _waveText.SetText( waveText );
    }
    public void SetTimerActive( bool isActive ) => _timerText.gameObject.SetActive( isActive );
    public InfoUIPanel GetInfoPanel() => _infoPanel.GetComponent<InfoUIPanel>();
    public FunctionalPanelPresenter GetFunctionalPanel() => _functionalPanel.GetComponent<FunctionalPanelPresenter>();

    public void ShowWorldMapButton() => _worldMap.transform.parent.gameObject.SetActive( true );
    public void HideBattleButton()   => _battleStart.gameObject.SetActive( false );

    public void ShowGameOverPanel()  => _gameOverPanel.gameObject.SetActive(true);

    public void TowerBarAutoResize(){
        var content = _towerBar.GetComponent<ScrollRect>().content;
        var grid = content.GetComponent<GridLayoutGroup>();
        var cellHeight = grid.cellSize.y;
        var newHeight  = grid.preferredHeight + cellHeight;
        content.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, newHeight);
    }
    public void TowerBarSwitch(){
        _towerBarIsFold        = !_towerBarIsFold;
        var barFoldRect        = _barFold.transform.Find( "Icon" ).GetComponent<RectTransform>();
        var newScale           = barFoldRect.localScale;
        newScale.x             = -newScale.x;
        barFoldRect.localScale = newScale;

        var animator              = _towerBar.GetComponent<Animator>();
        var animationName         = _towerBarIsFold? "Fold": "Extend";
        var currentNormalizedTime = animator.GetCurrentAnimatorStateInfo(0).IsName( "None" )? 1 : animator.GetCurrentAnimatorStateInfo(0).normalizedTime;
        
        animator.Play(animationName, 0, 1 - currentNormalizedTime);
    }
    public void TowerBarInit(){
        _towerBarIsFold = false;
        var barFoldRect = _barFold.transform.Find( "Icon" ).GetComponent<RectTransform>();
        var newScale    = barFoldRect.localScale;
        newScale.x = -Mathf.Abs(newScale.x);
        barFoldRect.localScale = newScale;
        _towerBar.GetComponent<RectTransform>().SetSizeWithCurrentAnchors( RectTransform.Axis.Horizontal, 420 );
    }

    public void WhenSceneChanged( GameManager.GameState gameState ){
        Init();
        switch( gameState ){
            case GameManager.GameState.WorldMap:
                gameObject.SetActive( false );
                break;
            case GameManager.GameState.Prepare:
                GetComponent<Canvas>().worldCamera = Camera.main;
                gameObject.SetActive( true );
                break;
        }
    }

    public GameObject AddItemIntoInventory(GameObject inventorySlotPrefab, InventorySystem.InventorySlot slot){
        var inventorySlot = Instantiate(inventorySlotPrefab, _inventoryContent);
        var contentGroup  = _inventoryContent.GetComponent<GridLayoutGroup>();
        inventorySlot.GetComponent<GridLayoutGroup>().cellSize = contentGroup.cellSize * 0.8f;
        _inventoryContent.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, contentGroup.preferredHeight + contentGroup.cellSize.y);

        var invnetoryItem = inventorySlot.transform.Find("Mask/InventoryItem");
        invnetoryItem.GetComponent<BoxCollider2D>().size = inventorySlot.GetComponent<GridLayoutGroup>().cellSize;
        invnetoryItem.GetComponent<Image>().sprite       = slot.Item.Icon;
        invnetoryItem.GetComponent<RectTransform>().localPosition += Vector3.back;
        Destroy(invnetoryItem.GetComponent<ItemInfo>());
        var selected = invnetoryItem.gameObject.AddComponent<SelectedItem>();
        selected.SetSlot(slot);
        selected.SetMenuPresenter(_functionalPanel.GetComponent<FunctionalPanelPresenter>());
        selected.SellingItem += GameManager.Instance.newPlayer.MakeMoney;
        return inventorySlot;
    }

    public void OnSaveClicked(){
        SaveData data = GameManager.Instance.newPlayer.GetSaveData();
        SaveSystem.Save(data, "SaveData01");
    }

    public void OnInventoryClicked(){
        _inventoryPanel.SetActive(true);
        _inventoryIcon.gameObject.SetActive(false);
    }
    public void OnInventoryClosed(){
        _inventoryPanel.SetActive(false);
        _inventoryIcon.gameObject.SetActive(true);
    }
    #endregion

}
