using UnityEngine;

public class EnemyPresenter : MonoBehaviour
{
    #region Private Variable
    // [SerializeField] private NewEnemy    model;
    // [SerializeField] private HpBar       enemyHpUI;
    // [SerializeField] private Enemy1DView enemy1DView;
    #endregion

    #region Property
    private NewEnemy    model       => GetComponent<NewEnemy>();
    private HpBar       enemyHpUI   => GetComponentInChildren<HpBar>();
    private Enemy1DView enemy1DView => GetComponentInChildren<Enemy1DView>();
    #endregion

    private void Awake(){
        model.OnHealthChanged  += ( currentHealth ) => enemyHpUI.UpdateUI( model.heathPercent );
        model.DirectionChanged += enemy1DView.UpdateEnemyAnimator;
    }
}
