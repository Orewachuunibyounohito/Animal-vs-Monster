using UnityEngine;

public static class PrefabRepository
{
    public static GameObject      GameManager{ get; private set; }
    public static GameplayUIPanel GameplayUI{ get; private set; }
    public static NewPlayer       Player{ get; private set; }
    public static LoadingUIPanel  LoadingUIPanel{ get; private set; }
    public static BuildManager    BuildManager{ get; private set; }
    public static GameObject      AliveEnemyText{ get; private set; }
    public static GameObject      InventorySlot{ get; private set; }
    public static GameObject      AttackArea{ get; private set; }
    public static GameObject      Thank { get; private set; }

    static PrefabRepository(){
        var prefabSettings = Resources.Load<PrefabSettingsSo>("TD/Prefabs/Prefab Settings");
        GameManager    = prefabSettings.GameManager;
        GameplayUI     = prefabSettings.GameplayUi.GetComponent<GameplayUIPanel>();
        Player         = prefabSettings.NewPlayer.GetComponent<NewPlayer>();
        LoadingUIPanel = prefabSettings.LoadingUIPanel.GetComponent<LoadingUIPanel>();
        BuildManager   = prefabSettings.BuildManager.GetComponent<BuildManager>();
        AliveEnemyText = prefabSettings.AliveEnemyText;
        InventorySlot  = prefabSettings.InventorySlot;
        AttackArea     = prefabSettings.AttackArea;
        Thank          = prefabSettings.Thank;
    }
}