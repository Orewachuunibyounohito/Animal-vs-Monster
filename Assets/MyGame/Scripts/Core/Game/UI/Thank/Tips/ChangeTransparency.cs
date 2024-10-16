using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class ChangeTransparency : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private TextMeshProUGUI text;
    
    void Awake(){
        text = GetComponent<TextMeshProUGUI>();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        TextAppear();
    }
    
    public void OnPointerExit(PointerEventData eventData){
        TextDisappear();
    }
    
    private void TextAppear()
    {
        var color = text.color;
        color.a = 1;
        text.color = color;
    }
    private void TextDisappear()
    {
        var color = text.color;
        color.a = 0;
        text.color = color;
    }

}
