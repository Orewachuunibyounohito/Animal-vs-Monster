using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class MainScript : MonoBehaviour
{
    private InputFieldController controller;
    [SerializeField]
    private TMP_InputField       inputField;

    void Awake()
    {
        controller = new InputFieldController(inputField);
    }

    // Update is called once per frame
    void Update()
    {
        controller.HandleInputField();
    }
}

public class InputFieldController
{
    private IInputSystem   inputSystem;
    private TMP_InputField inputField;

    public InputFieldController(TMP_InputField inputField){
        inputSystem     = new InputFieldInput();
        this.inputField = inputField;
    }

    public void HandleInputField(){
        if(inputSystem.RetrieveActivateInputField()){ ActivateInputField(); }
        else if(inputSystem.RetrieveDeactivateInputField()){ DeactivateInputField(); }
    }

    private void ActivateInputField(){
        inputField.gameObject.SetActive(true);
        inputField.ActivateInputField();
    }
    private void DeactivateInputField(){
        inputField.DeactivateInputField();
        inputField.text = "";
        inputField.gameObject.SetActive(false);
    }
}

public class InputFieldInput : IInputSystem
{
    public bool RetrieveActivateInputField() => Keyboard.current.enterKey.wasPressedThisFrame;
    public bool RetrieveDeactivateInputField() => Keyboard.current.escapeKey.wasPressedThisFrame;
}

public interface IInputSystem
{
    bool RetrieveActivateInputField();
    bool RetrieveDeactivateInputField();
}