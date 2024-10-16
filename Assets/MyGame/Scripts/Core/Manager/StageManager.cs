using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StageManager : MonoBehaviour
{
    #region Private Variable

    [SerializeField] private GameObject world;
    [SerializeField] private Color activeColor, clearedColor;

    private Button _testStageButton;
    // private List<Button> _stagesBtn;
    private Dictionary<int, Button> _stagesBtn;
    
    #endregion

    #region Unity Events

    private void OnDisable(){
        
    }

    private void Awake(){
        StageButtonInit();
        
        var isFirstRoundFinish = GameManager.Instance.HasFinish && GameManager.Instance.Round == 1;
        if(isFirstRoundFinish){ Instantiate(PrefabRepository.Thank); }
    }
    private void Start(){
        foreach( var stageIndex in GameManager.Instance.newPlayer.ClearStage ){
            ButtonTurnToCleared(_stagesBtn[stageIndex]);
        }
        
        if(GameManager.Instance.newPlayer.TestStageCleared){
            ButtonTurnToCleared(_testStageButton);
        }
    }

    #endregion

    #region Private Methods
    
    private void StageButtonInit(){
        _stagesBtn = new Dictionary<int, Button>();
        var stagesButton = world.GetComponentsInChildren<Button>( true );
        var clearStage = GameManager.Instance.newPlayer.ClearStage;
        int maxId = 0;
        int stageId;
        TextMeshProUGUI text;
        foreach( var button in stagesButton ){
            if(button.name == "Test"){
                _testStageButton = button;

                _testStageButton.onClick.AddListener(delegate{
                    GameManager.Instance.EnterScene("TestStage");
                });
                continue;
            }

            button.onClick.AddListener( delegate{
                GameManager.Instance.EnterScene( button.name );
            } );
            stageId = int.Parse( button.name.Remove( 0, 5 ) );
            text    = button.GetComponentInChildren<TextMeshProUGUI>();
            text.SetText( $"Stage {stageId}" );
            if( clearStage.Contains( stageId ) ){
                button.gameObject.SetActive( true );
                ButtonTurnToActive(button);
            }else{ ButtonTurnToInactive(button); }
            maxId++;
            _stagesBtn.Add( stageId, button );
        }
        int maxClearId = clearStage.Count > 0? clearStage.Max() : 0;
        if( maxClearId == maxId ){ return ; }
        ButtonTurnToActive(_stagesBtn[maxClearId+1]);
    }

    private void ButtonTurnToInactive(Button button){
        button.interactable = false;
    }
    private void ButtonTurnToActive(Button button){
        button.interactable = true;
        button.GetComponent<Image>().color = activeColor;
    }
    private void ButtonTurnToCleared(Button button){
        button.interactable = true;
        button.GetComponent<Image>().color = clearedColor;
    }
        
    #endregion

    #region Helper Methods

    private void ShowAllButton(){
        string log = "[";
        foreach( var button in _stagesBtn ){
            log += $"{button.Value.name}, ";
        }
        log = log.Remove( log.Length-2 );
        log += "]";
        Debug.Log( log );
    }

    #endregion

}
