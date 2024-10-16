using UnityEngine;

public class MoveWithPathPresenter : MonoBehaviour
{
    #region Private Variable
    [SerializeField] private MoveWithPath _moveWithPath;
    [SerializeField] private Enemy1DView  _targetView;
    #endregion

    private void Awake(){
        _moveWithPath.OnDirectionChanged += _targetView.UpdateEnemyAnimator;
    }
}
