using Sirenix.OdinInspector;
using UnityEngine;

public abstract class InstantiateData : ScriptableObject
{
    protected const string SPLIT          = "Split";
    protected const string SPLIT_GENERAL  = "Split/General Settings";
    protected const string GENERAL_LEFT   = "Split/General Settings/Split";
    protected const int    PREVIEW_HEIGHT = 67;
    #region Field
    [HideLabel, PreviewField(PREVIEW_HEIGHT)]
    [PropertyOrder(-1)]
    [VerticalGroup(SPLIT)]
    [HorizontalGroup(GENERAL_LEFT, PREVIEW_HEIGHT, LabelWidth = PREVIEW_HEIGHT)]
    [BoxGroup(SPLIT_GENERAL)]
    [SerializeField] protected Sprite     _image;

    [VerticalGroup(GENERAL_LEFT + "/Right")]
    [BoxGroup(SPLIT_GENERAL)]
    [SerializeField] protected int        _id;

    [AssetsOnly]
    [VerticalGroup(GENERAL_LEFT + "/Right")]
    [BoxGroup(SPLIT_GENERAL)]
    [SerializeField] protected GameObject _prefab;
    #endregion

    #region Property
    public int        id       => _id;
    
    [ReadOnly, LabelText("Name")]
    [ShowInInspector]
    [VerticalGroup(GENERAL_LEFT + "/Right")]
    [PropertyOrder(-1)]
    [BoxGroup(SPLIT_GENERAL)]
    public string     dataName => _prefab.name;

    public Sprite     image    => _image;
    public GameObject prefab   => _prefab;
    #endregion
}
