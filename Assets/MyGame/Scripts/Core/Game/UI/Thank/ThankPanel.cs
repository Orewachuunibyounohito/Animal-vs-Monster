using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class ThankPanel : MonoBehaviour, IPointerClickHandler
{
    // private TextMeshProUGUI tips;

    // void Awake(){
    //     tips = transform.Find("Content/Tips").GetComponent<TextMeshProUGUI>();
    // }

    void Update()
    {
        if(Keyboard.current.anyKey.wasPressedThisFrame){
            GameContinue();
        }
    }

    public void OnPointerClick(PointerEventData eventData){
        GameContinue();
    }

    private void GameContinue(){
        GameManager.Instance.Round++;
        Destroy(gameObject);
    }
}
