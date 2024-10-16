using UnityEngine;

public interface IInfoUIPanel
{
    public string Name{ get; set; }
    public string Info{ get; }

    public void SetInfo(IInfoData info);
    // public void ChangePosition(Vector3 myMousePositionInWorld);
    public void ShowInfo();
    public void HideInfo();
}
