using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class EventSystemTestText : MonoBehaviour
{
    [SerializeField]
    private EventSystem     eventSystem;
    private TextMeshProUGUI text;

    void Awake() => text = GetComponent<TextMeshProUGUI>();

    void Update()
    {
        text.SetText(eventSystem.currentSelectedGameObject?.name);
    }
}
