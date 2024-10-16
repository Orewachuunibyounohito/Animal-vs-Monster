using UnityEngine;

public class SelectableComponenet : MonoBehaviour, ISelectableComponent
{
    protected InfoUIPanel infoUIPanel;
    protected IInfoData   infoData;

    protected virtual void Start(){
        infoUIPanel = GameManager.Instance.gameplayUI.GetInfoPanel();
        infoData    = GetComponent<IInfoData>();
    }

    protected virtual void OnDisable(){
        if(Equals(GameManager.Instance.SelectedSystem.SelectedComponent)){
            infoUIPanel.HideInfo();
            GameManager.Instance.SelectedSystem.LostSelected();
        }
    }

    public virtual void Deselect(){
        infoUIPanel.HideInfo();
        GameManager.Instance.SelectedSystem.LostSelected();
    }

    public virtual void Select(){
        infoUIPanel.SetInfo(infoData);
        infoUIPanel.ShowInfo();
    }
}
