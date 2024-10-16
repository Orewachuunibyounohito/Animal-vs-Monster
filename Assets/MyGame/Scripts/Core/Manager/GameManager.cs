using System;
using System.Collections;
using System.Collections.Generic;
using TD.Item;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{
    #region Enum
    public enum GameState{ WorldMap, Prepare, Waiting, Battling, Pause, GameOver }
    #endregion

    public readonly object aliveEnemyLock = new object();

    #region Field
    [SerializeField] private GameState  _gameState = GameState.WorldMap;
    [HideInInspector]
    [SerializeField] private Player     _player;
    /* [SerializeField] */ private NewPlayer  _newPlayer;

    [SerializeField] private SpawnEnemy _spawnEnemy;
    [SerializeField] private int        _aliveEnemy;
    [SerializeField] private ClearState _clear;
    [SerializeField] private bool       _isCheater;
    private bool   _isLoad;
    private bool   _hasFinish;
    private int    _round = 1;
    private string _selectedSaveSlot;
    private Stats  _openingStats;
    
    [HideInInspector]
    [SerializeField] private LibraryManager _library;
    
    /* [SerializeField] */ private GameplayUIPanel _gameplayUI;
    /* [SerializeField] */ private LoadingUIPanel  _loadingUIPanel;
    /* [SerializeField] */ private BuildManager    _buildManager;
    /* [SerializeField] */ private GameObject      _aliveEnemyText;

    private GiveTipSystem _giveTipSystem;

    #endregion

    #region Property
    public NewPlayer    newPlayer    => _newPlayer;
    public GameState    gameState    => _gameState;
    public SpawnEnemy   SpawnEnemy   => _spawnEnemy;
    public bool         HasFinish    => _hasFinish;
    public int          Round{ get => _round; set => _round = value;}

    public NewLibrarySystem Library => _library.LibrarySystem;
    public InventorySystem  InventorySystem{ get; private set; }
    public SelectedSystem   SelectedSystem{ get; private set; }

    public bool         NewMoneyEnough( int cost )   => _newPlayer.Money >= cost? true : false;
    public ItemData     GetItem( int id )            => GetComponent<ItemLibrary>()[id];
    public TowerData    GetTower( string towerName ) => GetComponent<TowerLibrary>()[towerName];

    public LoadingUIPanel  loadingPrefab  => _loadingUIPanel;
    public GameplayUIPanel gameplayUI     => _gameplayUI;
    public MallManager     MallManager{ get; private set; }
    public GameObject      AliveEnemyText => _aliveEnemyText;

    public AudioPlayer AudioPlayer{ get; private set; }
    public InputActions CustomInput{ get; private set; } 
    #endregion

    #region Const
    private const float WAVE_INTERVAL       = 1.5f;
    private const string AUDIO_SETTINGS_PATH = "TD/Audios/AudioSettings";
    #endregion

    #region Custom Events
    public UnityEvent<float>  OnTimeChanged;
    public UnityEvent<string> OnWaveChanged;
    public UnityEvent         OnStageClear;
    public UnityEvent         TowerBarChanged;
    public Action GameOver;
    #endregion

    #region Unity Events
    protected override void Awake()
    {
        base.Awake();
        _library       = GetComponent<LibraryManager>();

        InventorySystem = new InventorySystem(this);
        SelectedSystem  = new SelectedSystem();

        SaveSystem.SaveDirectory = "SaveData";

        var audioSettings = Resources.Load<AudioSettings>(AUDIO_SETTINGS_PATH);
        AudioPlayer       = new AudioPlayer(this, audioSettings);
        AudioPlayer.PlayBgm(BgmName.Menu);

        CustomInput = new InputActions();
        CustomInput.Gameplay.Enable();
        CustomInput.Gameplay.Interact.performed         += SelectedSystem.Selecting;
        CustomInput.Gameplay.InteractForTouch.performed += SelectedSystem.SelectingForTouch;
        CustomInput.Gameplay.ControlCamera.performed    += ActivateCameraControl;
    }

    private void Update(){
        #if UNITY_EDITOR
        if(Keyboard.current.iKey.wasPressedThisFrame){
            InventorySystem.Test_AddItem(_newPlayer.Inventory);
        }
        #endif
    }

    private void OnApplicationQuit(){
        Destroy(gameObject);
    }
    #endregion

    #region Check Level Clear
    public void EnemyDie(){
        lock( aliveEnemyLock ){
            _aliveEnemy--;
            HandleWaveClear();
        }
    }
    private void HandleWaveClear(){
        if( _aliveEnemy == 0 ){
            _gameState = GameState.Waiting;
            _giveTipSystem.Stop();
            OnWaveClear();
        }
    }
    private void OnWaveClear(){
        _clear.gameObject.SetActive( true );
        _clear.Enter();
    }
    public void StageClear(){
        UnlockItem_New( _spawnEnemy.LevelReward );

        try{
            string stageName = SceneManager.GetActiveScene().name;
            int    stageId   = -1;
            if(stageName == StageName.TestStage.ToString()){
                _newPlayer.TestStageCleared = true;
            }else{
                stageId = int.Parse( stageName.Substring( 5 ) );
                _newPlayer.ClearStage.Add( stageId );

                var isEnding = stageId == 2;
                if(isEnding){ _hasFinish = true; }
            }
        }catch(Exception ex){
            Debug.Log(ex);
        }

        OnStageClear.Invoke();
    }
    #endregion

    #region To Next Wave
    public void NextWave(){
        StartCoroutine( NextWaveCoroutine() );
    }
    IEnumerator NextWaveCoroutine(){
        float timer = WAVE_INTERVAL;
        float deltaTime = 0.1f;
        _gameplayUI.SetTimerActive( true );
        while( timer > 0 ){
            OnTimeChanged.Invoke( timer );
            yield return new WaitForSeconds( deltaTime );
            timer -= deltaTime;
        }
        _gameplayUI.SetTimerActive( false );
        _gameState = GameState.Battling;
        _giveTipSystem.Start();
        _spawnEnemy.NextSpawn();
        _aliveEnemy = _spawnEnemy.EnemyCount;
        OnWaveChanged.Invoke( _spawnEnemy.WaveText );
    }
    #endregion

    #region Unlock Item
    private void UnlockItem_New( RewardData reward ){
        if( reward == default ){
            Debug.Log( $"The stage no reward d(^ 一^)b." );
            return ;
        }
        _newPlayer.MakeMoney(reward.Coins);
        foreach( var item in reward.newItemData ){
            switch( item.itemType ){
                case ItemType.Coin:
                    // _newPlayer.MakeMoney(item.);
                    break;
                default:
                    MallManager.Add( item );
                    break;
            }
        }
    }
    #endregion

    #region Create Player & GameplayUI & BuildManager
    public void GameLoad(string saveSlot = "SaveData01"){
        _isLoad           = true;
        _selectedSaveSlot = saveSlot;
        GameStart();
    }
    public void GameStart(){
        InstantiateBuildManager();

        InstantiateGameplayUI();
        MallManager = _gameplayUI.transform.Find( "Mall" ).GetComponent<MallManager>();
        InstantiatePlayer();
        if(_isLoad){ SaveSystem.Load(_selectedSaveSlot); }

        gameObject.AddComponent<GameplayUIPresenter>();
        GameManagerBindingClearUI();
        PlayerBindingTowerBar();

        _giveTipSystem = new GiveTipSystem(_newPlayer);

        _newPlayer.WhenGameStart();
        EnterWorldMap();
    }
    private void InstantiatePlayer(){
        _newPlayer      = Instantiate( PrefabRepository.Player );

        int parenIndex  = _newPlayer.name.IndexOf( '(' );
        _newPlayer.name = parenIndex == -1? _newPlayer.name : _newPlayer.name.Remove(parenIndex);
        DontDestroyOnLoad( _newPlayer );

        #if UNITY_EDITOR
        _newPlayer.IsCheater = _isCheater;
        #endif
        _newPlayer.SetInit(20, 10);
        _newPlayer.Inventory.AmountChanged += InventorySystem.UpdateInventorySlotAmount;
        _newPlayer.Inventory.ItemUseUp     += InventorySystem.RemoveSlot;
        _newPlayer.LootingItem             += InventorySystem.AddItem;
        _newPlayer.Die                     += OnGameOver;
    }
    private void InstantiateGameplayUI(){
        _gameplayUI      = Instantiate( PrefabRepository.GameplayUI );

        int parenIndex   = _gameplayUI.name.IndexOf('(');
        _gameplayUI.name = parenIndex == -1? _gameplayUI.name : _gameplayUI.name.Remove(parenIndex);
        // _gameplayUI.gameObject.SetActive( false );
        DontDestroyOnLoad( _gameplayUI );

    }
    private void InstantiateBuildManager(){
        _buildManager = Instantiate( PrefabRepository.BuildManager );
        DontDestroyOnLoad( _buildManager );
    }
    private void GameManagerBindingClearUI(){
        _clear = _gameplayUI.transform.Find( "Clear" ).GetComponent<ClearState>();
    }
    private void PlayerBindingTowerBar(){
        _newPlayer.SetTowerBar( _gameplayUI.transform.Find( "TowerBar" ).gameObject );
    }
    #endregion

    public void OnGameOver(){
        _giveTipSystem.Stop();
        _gameplayUI.ShowGameOverPanel();
        GameOver?.Invoke();
    }

    #region Change Scene
    public void WhenSceneChanged( string sceneName ){
        switch( sceneName ){
            case "WorldMap":
                _gameState  = GameState.WorldMap;
                _spawnEnemy = null;
                AudioPlayer.PlayBgm(BgmName.WorldMap);
                break;
            default:
                _gameState  = GameState.Prepare;
                _spawnEnemy = GameObject.Find( "SpawnManager" ).GetComponent<SpawnEnemy>();
                var levelDatas = Resources.Load<LevelSettings>("TD/Stages/LevelSettings");
                var levelData  = levelDatas.Stages.Find((dataSet) => dataSet.StageName.ToString() == sceneName).LevelData;
                _spawnEnemy.SetLevelData(levelData);
                _openingStats = new Stats{ Hp = _newPlayer.CurrentHeath, Coin = _newPlayer.Money };
                AudioPlayer.PlayBgm(BgmName.Battle);
                break;
        }
        _newPlayer.WhenSceneChanged( _gameState );
        _gameplayUI.WhenSceneChanged( _gameState );
        BuildManager.Instance.WhenSceneChanged( _gameState );
        BuildMode.Instance.WhenSceneChanged( gameState );
    }
    public void EnterScene( string sceneName ){
        StartCoroutine( LoadSceneAsync( sceneName ) );
    }
    public void EnterWorldMap(){
        StartCoroutine( LoadSceneAsync( "WorldMap" ) );
    }

    private IEnumerator LoadSceneAsync( string sceneName ){
        var loading = Instantiate( PrefabRepository.LoadingUIPanel );
        DontDestroyOnLoad( loading.gameObject );
        var async = SceneManager.LoadSceneAsync( sceneName );
        while( !async.isDone ){
            loading.UpdateProgressBar( async.progress );
            yield return null;
        }
        Destroy( loading.gameObject );

        WhenSceneChanged( sceneName );
    }
    #endregion
    
    public void OnStageRetry(){
        StatsReset();
        EnterScene(SceneManager.GetActiveScene().name);
    }
    public void OnGameOverBack(){
        StatsReset();
        EnterWorldMap();
    }
    private void StatsReset(){
        _newPlayer.ChangeHp(_openingStats.Hp);
        _newPlayer.Consume(_newPlayer.Money - _openingStats.Coin);
    }

    public void InstantiateAliveEnemyText(Vector2 position){
        var text = Instantiate(PrefabRepository.AliveEnemyText, position, Quaternion.identity);
        text.GetComponentInChildren<TMP_Text>().SetText( $"Enemies: {_aliveEnemy}/{_spawnEnemy.EnemyCount}" );
    }

    public void ActivateCameraControl(InputAction.CallbackContext callback){
        if(callback.performed){
            Debug.Log($"{callback.action}");
            CustomInput.Camera.Enable();
        }
    }

    public GameManager InstantiatePlayer_Test(){
        InstantiatePlayer();
        return this;
    }

    [Serializable]
    private class Stats
    {
        public int Hp;
        public int Coin;
    }
}
