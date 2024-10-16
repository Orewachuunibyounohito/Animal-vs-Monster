using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.EventSystems;

public class ShowInfo : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [ShowInInspector]
    private InfoUIPanel _infoUI;

    [ShowInInspector]
    private IInfoData   _infoData;
    
    void Awake(){
        _infoUI   = GameManager.Instance.gameplayUI.GetInfoPanel();
        _infoData = transform.Find("Mask/InventoryItem").GetComponent<IInfoData>();
    }
    void Start(){
    }

    void Update(){
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        _infoUI.SetInfo(_infoData);
        _infoUI.ShowInfo();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        _infoUI.HideInfo();
    }
}
