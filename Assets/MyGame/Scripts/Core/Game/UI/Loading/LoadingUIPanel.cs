using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LoadingUIPanel : MonoBehaviour
{
    #region Private Variable
    [SerializeField] private List<TMP_Text> loadingTexts;
    [SerializeField] private Image _progressBar;
    #endregion

    #region Unity Events
    private void Awake(){
        _progressBar = transform.Find( "ProgressBar/Bar" ).GetComponent<Image>();
    }
    #endregion

    #region Public Methods
    public void UpdateProgressBar( float percent ){
        _progressBar.fillAmount = percent;
    }
    #endregion
}
