using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuUIPanel : MonoBehaviour
{   
    #region Property
    public Button     Start{ get; private set; }
    public Button     Load { get; private set; }
    public Button     Exit{ get; private set; }
    public Button     ExitYes{ get; private set; }
    public Button     ExitNo{ get; private set; }
    public Button     ExitX{ get; private set; }
    public Button     NewNameConfirm{ get; private set; }

    [ShowInInspector]
    public TMP_InputField NewNameInput{ get; private set; }

    public GameObject ExitPanel{ get; private set; }
    public GameObject NewNamePanel{ get; private set; }
    #endregion

    #region Unity Events
    private void Awake(){
        AssingPanels();
        AssignButtons();
        AssignInputField();
    }
    #endregion

    #region Private Methods
    private void AssingPanels(){
        ExitPanel    = transform.Find( "ExitDialogPanel" ).gameObject;
        NewNamePanel = transform.Find( "NewNamePanel" ).gameObject;
    }
    private void AssignButtons(){
        foreach( var button in GetComponentsInChildren<Button>( true ) ){
            switch( button.name ){
                case "Start":   Start          = button; break;
                case "Load":    Load           = button; break;
                case "Exit":    Exit           = button; break;
                case "Yes":     ExitYes        = button; break;
                case "No":      ExitNo         = button; break;
                case "Cancel":  ExitX          = button; break;
                case "Confirm": NewNameConfirm = button; break;
            }
        }
    }
    private void AssignInputField(){
        foreach(var inputField in GetComponentsInChildren<TMP_InputField>(true)){
            switch( inputField.name ){
                case "InputField": NewNameInput = inputField; break;
            }
        }
    }
    #endregion
}
