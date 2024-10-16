using System.Collections;
using System.Collections.Generic;
using Refactoring;
using TMPro;
using UnityEngine;

public class TextMain : MonoBehaviour
{
    private NewInfoSystem infoSystem;
    [SerializeField]
    private TextMeshProUGUI text;
    
    private void Awake(){
        infoSystem = new NewInfoSystem(text);
    }

    private void Start(){
        infoSystem.Add(NewInfoSystem.DefaultArticle);
        infoSystem.DisplayInfo();
    }
}
