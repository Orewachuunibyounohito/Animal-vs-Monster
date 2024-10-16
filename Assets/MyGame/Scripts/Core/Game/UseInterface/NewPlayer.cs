using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using TD.Item;
using UnityEngine;
using UnityEngine.Events;
using Random = UnityEngine.Random;

public class NewPlayer : NewDamageable
{
    private readonly object moneyLock = new object();

    #region Field
    [SerializeField] private int             _money;
    [SerializeField] private string          _playerName;
    [SerializeField] private GameObject      _towerBar;
    [SerializeField] private TowerSlot       _towerSlotPrefab;
    [SerializeField] private bool            _isCheater = false;

    // [SerializeField] private Dictionary<string, TowerSlot> _ownedTower = new Dictionary<string, TowerSlot>();
    [SerializeField] private HashSet<string> _ownedTower = new HashSet<string>();
    [SerializeField] private HashSet<int>    _clearStage;
    [SerializeField] private InventorySystem.Inventory   _inventory = new InventorySystem.Inventory();
    #endregion

    #region Property
    public int          CurrentHeath => _currentHeath;
    public int          Money        => _money;
    public HashSet<int> ClearStage   => _clearStage;
    public bool         ChangeScene{ get; set; }
    public bool         IsCheater{ get => _isCheater; set => _isCheater = value; }
    public InventorySystem.Inventory Inventory => _inventory;
    private Transform _towerBarContent => _towerBar.GetComponent<UnityEngine.UI.ScrollRect>().content;

    public bool TestStageCleared{ get; set; }
    #endregion

    #region Const
    private const int NORMAL_ENEMY_DAMAGE = 1;
    private const int STRONG_ENEMY_DAMAGE = 5;
    private const int BOSS_DAMAGE = 20;
    #endregion

    #region Custom Events
    public UnityAction<int> OnMoneyChanged;
    public event Action     TowerBarChanged;
    public event Action<string> NameChanged;
    public event Action<InventorySystem.Inventory, Item, int> LootingItem;

    public delegate void DieEvent();
    public DieEvent Die;
    #endregion

    #region Unity Events
    protected override void Awake()
    {
        SetInit(100, 20);
        _clearStage = new HashSet<int>();
    }

    public void SetInit(int maxHp, int  money)
    {
        _maxHealth = maxHp;
        _currentHeath = _maxHealth;
        _money = money;
    }
    #endregion

    #region Earn & Cost
    public void Consume( int amount ){
        lock( moneyLock ){
            _money -= amount;
            OnMoneyChanged?.Invoke( _money );
        }
    }
    public void MakeMoney( int amount ){
        lock( moneyLock ){
            _money += amount;
            OnMoneyChanged?.Invoke( _money );
        }
    }
    #endregion

    #region Loot
    private void LootItem( NewItemData itemData, int amount ){
        if(itemData.itemType == ItemType.Coin){
            MakeMoney(amount);
            return ;
        }

        var item = ItemFactory.GenerateItem(itemData.name);
        if( itemData.stackable ){
            LootingItem.Invoke(_inventory, item, amount);
            return ;
        }

        for( int cnt = amount; cnt > 0; cnt-- ){
            LootingItem.Invoke(_inventory, item, 1);
        }
    }

    public void Loot( NewEnemy enemy ){
        if( enemy.reward == default ){
            Debug.Log( "Hehe, this enemy no reward. (+ m +)p" );
            return ;
        }
        foreach( var loot in enemy.reward.newItemData ){
            if( !HasDrop( loot, enemy.strength ) ){ return ; }

            int amount = DropAmount( loot, enemy.strength );
            LootItem(loot, amount);
        }
    }
    #endregion

    #region Check Drop or Not
    private bool HasDrop( NewItemData itemData, ulong strength ){
        float value     = Random.Range( 0f, 1f );
        float threshold = strength/(float)itemData.dropRateRare;
        // Drop item
        return value <= threshold? true : false;
    }
    private int DropAmount( NewItemData itemData, ulong strength){
        int   dropAmount = (int)( strength/(float)itemData.dropAmountRare*Random.Range( 1-itemData.DropRange, 1 ) );
        return dropAmount >= itemData.CountLeast? dropAmount : itemData.CountLeast;
    }
    #endregion

    public override void DealDamage(int damage)
    {
        base.DealDamage(damage);
        if(_currentHeath == 0){ Die.Invoke(); }
    }

    #region Enemy Enter Hurt Area
    public void EnemyEnter(){
        // TODO
        // enemy type
        DealDamage( NORMAL_ENEMY_DAMAGE );
    }
    public void EnemyEnterWithType( EnemyType enemyType ){
        switch( enemyType ){
            case EnemyType.Normal:
                DealDamage( NORMAL_ENEMY_DAMAGE );
                break;
            case EnemyType.Strong:
                DealDamage( STRONG_ENEMY_DAMAGE );
                break;
            case EnemyType.Boss:
                DealDamage( BOSS_DAMAGE );
                break;
        }
    }
    #endregion

    #region Spawn Owned TowerItem
    private void DefaultNewTowerBar(){
        _ownedTower = new HashSet<string>();
        ActivateTower( TowerName.Dude.ToString() );
        if( _isCheater ){
            ActivateTower( TowerName.Warrior.ToString() );
        }
    }
    public bool ActivateTower( string towerName ){
        if( _ownedTower.Contains( towerName ) ){
            Debug.Log( $"Tower {towerName} had activated." );
            return false;
        }

        TowerSlot towerSlot = Instantiate( _towerSlotPrefab, _towerBarContent );
        towerSlot.GenerateTowerItem(GameManager.Instance.Library.GetData<NewTowerData>(towerName));
        _ownedTower.Add(towerName.ToString());
        TowerBarChanged?.Invoke();
        return true;
    }
    private void ActivateOwnedTowers()
    {
        NewTowerData towerData;
        TowerSlot towerSlot;
        foreach (var owned in _ownedTower)
        {
            towerData = GameManager.Instance.Library.GetData<NewTowerData>(owned);
            towerSlot = Instantiate( _towerSlotPrefab, _towerBarContent );
            towerSlot.GenerateTowerItem( towerData );
        }
        TowerBarChanged?.Invoke();
    }
    #endregion

    #region Change Scene
    public void WhenGameStart(){
        bool isNewGame = _ownedTower.Count == 0;
        if(isNewGame){ DefaultNewTowerBar(); }
        else         { ActivateOwnedTowers(); }

        CustomEventsBinding();
    }

    public void WhenSceneChanged( GameManager.GameState gameState ){
        if( gameState == GameManager.GameState.Prepare ){
            OnHealthChanged?.Invoke( _currentHeath );
            OnMoneyChanged?.Invoke( _money );
            NameChanged?.Invoke(_playerName);
            GetComponent<Canvas>().worldCamera = Camera.main;
            GetComponent<PlayerUIPanel>().SetVisible( true );
        }else{
            GetComponent<PlayerUIPanel>().SetVisible( false );
        }
    }
    #endregion

    #region Events Binding
    private void CustomEventsBinding(){
        BuildManager.Instance.OnBuy.AddListener( Consume );
    }
    #endregion

    #region Set TowerBar
    public void SetTowerBar( GameObject target ) => _towerBar = target;
    #endregion

    public void SetPlayerName(string playerName){
        _playerName = playerName;
        NameChanged?.Invoke(_playerName);
    }

    public SaveData GetSaveData(){
        SaveData data = new SaveData(){
            name               = _playerName,
            money              = _money,
            maxHp              = _maxHealth,
            currentHp          = _currentHeath,
            clearedStage = _clearStage.ToList(),
            ownedTower   = _ownedTower.ToList(),
        };
        return data;
    }
    public void LoadData(SaveData data){
        // if(data == null){ return ; }
        if(data == null){
            throw new SaveDataNullException("SaveData is Null!");
        }
        _money      = data.money;
        _playerName = data.name;
        _maxHealth  = data.maxHp;
        _currentHeath = data.currentHp != 0? data.currentHp : data.maxHp;

        _ownedTower = new HashSet<string>();
        foreach(var owned in data.ownedTower){
            _ownedTower.Add(owned);
        }
        
        _clearStage = new HashSet<int>();
        foreach(var stage in data.clearedStage){
            _clearStage.Add(stage);
        }
        
        Debug.Log($"Load Success! {data.dateTime}");
    }

    #region Help Function
    #endregion

}

[System.Serializable]
public class SaveDataNullException : System.NullReferenceException
{
    public SaveDataNullException():base(){}

    public SaveDataNullException(string message) : base(message){}

    public SaveDataNullException(string message, System.Exception innerException) : base(message, innerException){}

    protected SaveDataNullException(SerializationInfo info, StreamingContext context) : base(info, context){}
}