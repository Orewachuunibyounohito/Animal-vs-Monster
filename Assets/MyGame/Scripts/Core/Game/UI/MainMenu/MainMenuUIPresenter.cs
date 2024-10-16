using System;
using System.Collections;
using System.IO;
using UnityEngine;
using UnityEngine.InputSystem;

public class MainMenuUIPresenter : MonoBehaviour
{
    #region Private Variable
    [SerializeField]
    private MenuManager     _menuManager;
    [SerializeField]
    private MainMenuUIPanel _uiPanel;
    #endregion

    #region Unity Events
    private void Awake(){
        UIBinding();

        _uiPanel.Start.onClick.AddListener(OnStartClicked);
        _uiPanel.Load.onClick.AddListener(OnLoadClicked);
        _uiPanel.Exit.onClick.AddListener(OnExitClicked);
        _uiPanel.NewNameConfirm.onClick.AddListener(OnConfirmClicked);

        if(TouchScreenKeyboard.isSupported){ _uiPanel.NewNameInput.onSelect.AddListener(OnInputFieldSelect); }
        
        // Exit Panel
        _uiPanel.ExitYes.onClick.AddListener(OnExitYesClicked);
        _uiPanel.ExitNo.onClick.AddListener(OnExitNoClicked);
        _uiPanel.ExitX.onClick.AddListener(OnExitNoClicked);
    }
    #endregion

    private void UIBinding(){
        _menuManager = GameObject.Find( "MenuManager" ).GetComponent<MenuManager>();
        _uiPanel     = GameObject.Find( "MainMenuUI" ).GetComponent<MainMenuUIPanel>();
    }

    
    #region Public Methods
    public void OnStartClicked()   => _uiPanel.NewNamePanel.SetActive(true);
    public void OnLoadClicked()    => GameManager.Instance.GameLoad("SaveData01");
    public void OnExitClicked()    => _uiPanel.ExitPanel.SetActive( true );
    public void OnInputFieldSelect(string message){
        StartCoroutine(touchKeyboardTask());
    }
    public void OnConfirmClicked(){
        string playerName = _uiPanel.NewNameInput.text;
        _uiPanel.NewNamePanel.SetActive(false);
        if (playerName == ""){
            Debug.Log("Player Name can't not be empty!");
            return;
        }
        _menuManager.EnterWorldMap();
        GameManager.Instance.newPlayer.SetPlayerName(playerName);
    }
    public void OnExitYesClicked() => _menuManager.GameExit();
    public void OnExitNoClicked()  => _uiPanel.ExitPanel.SetActive( false );
    #endregion

    private IEnumerator touchKeyboardTask(){
        if(TouchScreenKeyboard.visible){ yield break; }
        var screenKeyboard = TouchScreenKeyboard.Open("", TouchScreenKeyboardType.Default, false, false, false, true, "Player name...");
        while(screenKeyboard.status != TouchScreenKeyboard.Status.Done){
            yield return new WaitForFixedUpdate();
            if(screenKeyboard.status == TouchScreenKeyboard.Status.Canceled){
                _uiPanel.NewNameInput.text = "";
                yield break;
            }
            _uiPanel.NewNameInput.text = screenKeyboard.text;
        }
    }
}
