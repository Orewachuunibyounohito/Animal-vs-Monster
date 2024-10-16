using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventoryPanel : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    public Button        CloseButton{ get; private set; }
    public RectTransform Content{ get; private set; }

    private Vector2 offsetForMouseAndPanel;

    public void OnBeginDrag(PointerEventData eventData){
        var mouseInWorld = Camera.main.ScreenToWorldPoint(eventData.position);
        offsetForMouseAndPanel = transform.position - mouseInWorld;
    }

    public void OnDrag(PointerEventData eventData)
    {
        var mouseInWorld = Camera.main.ScreenToWorldPoint(eventData.position);
        transform.position = mouseInWorld.AsVector2() + offsetForMouseAndPanel;
        transform.localPosition += new Vector3(0, 0, -1);
    }

    public void OnEndDrag(PointerEventData eventData)
    {}

    void Awake()
    {
        CloseButton = transform.Find("Close").GetComponent<Button>();
        Content     = transform.Find("Items").GetComponent<ScrollRect>().content;
    }
}
