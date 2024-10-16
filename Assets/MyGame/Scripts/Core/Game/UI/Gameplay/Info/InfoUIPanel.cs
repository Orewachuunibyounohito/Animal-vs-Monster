using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class InfoUIPanel : MonoBehaviour, IInfoUIPanel
{   
    [SerializeField]
    private TextMeshProUGUI _nameText;
    [SerializeField]
    private List<InfoOneLine> _lines;
    private int lineCount;

    public string Name { get => _nameText.text; set => _nameText.SetText(value); }
    public string Info{ get; set; }

    public event Action OnShowInfo, OnHideInfo;

    private void Awake(){
        _nameText = transform.Find("Content/Name").GetComponent<TextMeshProUGUI>();
        var info = transform.Find("Content/Info");
        _lines     = info.Find("Viewport/Content").GetComponentsInChildren<InfoOneLine>().ToList();
    }

    public void SetInfo(IInfoData infoData){
        Name = infoData.Name;
        var labels = infoData.Detail[0].Split('\n');
        var values = infoData.Detail[1].Split('\n');
        var index = 0;
        for(; index < labels.Length; index++){
            _lines[index].LabelText = labels[index];
            _lines[index].ValueText = values[index];
            _lines[index].gameObject.SetActive(true);
        }
        lineCount = index;
        for(; index < _lines.Count; index++){
            _lines[index].Reset();
            _lines[index].gameObject.SetActive(false);
        }
    }

    public void HideInfo(){
        OnHideInfo.Invoke();
    }

    public void ShowInfo(){
        OnShowInfo.Invoke();
    }
}
