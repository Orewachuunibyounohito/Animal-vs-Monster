using System.Collections;
using UnityEngine;
using UnityEngine.UI;

using MyScripts.Tween;

public class HpBar : MonoBehaviour
{
    #region Field
    [SerializeField] private Image _hpBar;
    [SerializeField] private Image _hpDelay;
    [SerializeField] private bool  _useDelay     = true;
    [SerializeField] private bool  _useDelayFade = true;
    [SerializeField] private float _delayDuration = 2f;
    [SerializeField] private float _delayFadeDuration = 2f;
    private int delayCount = 0;
    #endregion

    private void Awake(){
        foreach( var image in GetComponentsInChildren<Image>() ){
            switch( image.name ){
                case "HpBar":
                    _hpBar = image;
                    break;
                case "HpDelay":
                    _hpDelay = image;
                    break;
            }
        }
    }

    #region Update UI
    public void UpdateUI( float percent ){
        _hpBar.fillAmount = percent;

        if( _useDelay ){
            Delay( _useDelayFade );
            return;
        }
        
        UpdateDelay( _hpBar.fillAmount );
    }
    private void UpdateDelay( float percent ){
        _hpDelay.fillAmount = percent;
    }
    #endregion

    #region Delay Function
    private void Delay( bool useFade=false ){
        StartCoroutine( DelayCoroutine( useFade ) );
    }
    private IEnumerator DelayCoroutine( bool useFade ){
        delayCount++;
        yield return new WaitForSeconds( _delayDuration );
        delayCount--;
        if( delayCount == 0 ){
            if( useFade ){
                GetComponent<Tween>().MyTween( _delayFadeDuration, _hpDelay.fillAmount, _hpBar.fillAmount, UpdateDelay );
            }else{
                UpdateDelay( _hpBar.fillAmount );
            }
        }
    }
    #endregion

    #region Test Function
    public void FillUpUI(){
        _hpBar.fillAmount = 1;        
        UpdateDelay( _hpBar.fillAmount );
    }
    #endregion

}
