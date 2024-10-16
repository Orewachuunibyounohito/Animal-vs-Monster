using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Player : Damageable
{
    // private readonly object moneyLock = new object();

    // #region Field
    // [SerializeField] private int             _money;
    // [SerializeField] private TextMeshProUGUI _moneyText;
    // [SerializeField] private TextMeshProUGUI _hpText;
    // [SerializeField] private string          _playerName;
    // [SerializeField] private GameObject      _towerBar;
    // [SerializeField] private TowerSlot       _towerSlotPrefab;
    // [SerializeField] private bool            _isCheater = false;

    // [SerializeField] private Dictionary<string, TowerSlot> _ownedTower = new Dictionary<string, TowerSlot>();
    // // [SerializeField] private Inventory   _inventory;
    // #endregion

    // #region Property
    // public int          money      => _money;
    // // public Inventory inventory => _inventory;
    // private Transform _towerBarContent => _towerBar.GetComponent<UnityEngine.UI.ScrollRect>().content;
    // #endregion

    // #region Const
    // private const int NORMAL_ENEMY_DAMAGE = 1;
    // private const int STRONG_ENEMY_DAMAGE = 5;
    // private const int BOSS_DAMAGE = 20;
    // #endregion

    // protected override void Awake(){
    //     _money = 20;
    //     _hp    = _maxHp;
    //     UpdateMoneyUI();
    //     UpdateHpUI();
    // }

    // private void Start(){
    //     // ActivateOwnedTower();
    //     DefaultTowerBar();
    //     BuildManager.Instance.OnBuy.AddListener( Consume );
    //     InputManager.Instance.OnSellTower.AddListener( MakeMoney );
    // }

    // #region Earn & Cost
    // public void EarnTip( int amount ){
    //     lock( moneyLock ){
    //         _money += amount;
    //         UpdateMoneyUI();
    //     }
    // }
    // public void Consume( int amount ){
    //     lock( moneyLock ){
    //         _money -= amount;
    //         UpdateMoneyUI();
    //     }
    // }
    // public void MakeMoney( int amount ){
    //     lock( moneyLock ){
    //         _money += amount;
    //         UpdateMoneyUI();
    //     }
    // }
    // #endregion

    // #region Loot
    // private void LootCoin( int amount ){
    //     lock( moneyLock ){
    //         _money += amount;
    //         UpdateMoneyUI();
    //     }
    // }
    // private void LootItem( ItemData itemData, int amount ){
    //     if( itemData.stackable ){
    //         // inventorySlot.AddItem( itemData, amount );
    //     }else{
    //         for( int cnt = amount; cnt >0; cnt-- ){
    //         //     inventorySlot.AddItem( itemData, 1 );
    //         }
    //     }
    // }
    // public void Loot( Enemy enemy ){
    //     if( enemy.reward == default ){
    //         Debug.Log( "Hehe, this enemy no reward. (+ m +)p" );
    //         return ;
    //     }
    //     foreach( var loot in enemy.reward.itemData ){
    //         if( HasDrop( loot, enemy.strength ) ){
    //             int count = DropCount( loot, enemy.strength );

    //             switch( loot.itemType ){
    //                 case ItemType.Coin:
    //                     LootCoin( count );
    //                     break;
    //                 default:
    //                     LootItem( loot, count );
    //                     break;
    //             }
    //         }
    //     }
    // }
    // // --- Test ---
    // public void Loot( NewEnemy enemy ){
    //     if( enemy.reward == default ){
    //         Debug.Log( "Hehe, this enemy no reward. (+ m +)p" );
    //         return ;
    //     }
    //     foreach( var loot in enemy.reward.itemData ){
    //         if( HasDrop( loot, enemy.strength ) ){
    //             int count = DropCount( loot, enemy.strength );

    //             switch( loot.itemType ){
    //                 case ItemType.Coin:
    //                     LootCoin( count );
    //                     break;
    //                 default:
    //                     LootItem( loot, count );
    //                     break;
    //             }
    //         }
    //     }
    // }
    // #endregion

    // #region Check Drop or Not
    // private bool HasDrop( ItemData itemData, ulong strength ){
    //     float value     = Random.Range( 0f, 1f );
    //     float threshold = strength/(float)itemData.dropRateRare;
    //     // Drop item
    //     return value <= threshold? true : false;
    // }
    // private int DropCount( ItemData itemData, ulong strength){
    //     int   dropCount = (int)( strength/(float)itemData.dropCountRare*Random.Range( 1-itemData.DropRange, 1 ) );
    //     return dropCount >= itemData.CountLeast? dropCount : itemData.CountLeast;
    // }
    // #endregion

    // #region Update UI
    // // No use Hp Bar
    // private void UpdateHpUI(){
    //     _hpText.SetText( _hp.ToString() );
    // }
    // private void UpdateMoneyUI(){
    //     _moneyText.SetText( _money.ToString() );
    // }
    // #endregion

    // #region Enemy Enter Hurt Area
    // public void EnemyEnter(){
    //     // TODO
    //     // enemy type
    //     DealDamage( NORMAL_ENEMY_DAMAGE );
    //     UpdateHpUI();
    // }
    // public void EnemyEnterWithType( EnemyType enemyType ){
    //     switch( enemyType ){
    //         case EnemyType.Normal:
    //             DealDamage( NORMAL_ENEMY_DAMAGE );
    //             break;
    //         case EnemyType.Strong:
    //             DealDamage( STRONG_ENEMY_DAMAGE );
    //             break;
    //         case EnemyType.Boss:
    //             DealDamage( BOSS_DAMAGE );
    //             break;
    //     }
    //     UpdateHpUI();
    // }
    // #endregion

    // #region Spawn Owned TowerItem
    // private void DefaultTowerBar(){
    //     _ownedTower = new Dictionary<string, TowerSlot>();
    //     // ActivateTower( GameManager.Instance.towerLibrary.TowerDictionary["Dude"] );
    //     // ActivateTower( GameManager.Instance.Library.TowerDictionary["Dude"] );
    //     if( _isCheater ){
    //         // ActivateTower( GameManager.Instance.towerLibrary.TowerDictionary["Warrior"] );
    //     }
    // }
    // public bool ActivateTower( TowerData towerData ){
    //     if( _ownedTower.ContainsKey( towerData.towerPrefab.name ) ){
    //         Debug.Log( $"Tower {towerData.towerPrefab.name} had activated." );
    //         return false;
    //     }

    //     TowerSlot towerSlot = Instantiate( _towerSlotPrefab, _towerBarContent );
    //     towerSlot.GenerateTowerItem( towerData );
    //     _ownedTower.Add( towerData.towerPrefab.name, towerSlot );
    //     return true;
    // }
    // #endregion

    // #region Help Function
    // /*
    // private void TotalDefeatTest(){
    //     _totalDefeat += 1;
    // }

    // [ContextMenu( "Show Total Earn" )]
    // private void ShowTotal(){
    //     Debug.Log( $"Total Defeat {_totalDefeat} in this level." );
    // }
    // */
    // #endregion

}
