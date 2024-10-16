using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class InfoOneLine : MonoBehaviour
{
    private TextMeshProUGUI _labelText, _valueText;

    public string LabelText{ 
        get => _labelText.text;
        set => _labelText.text = value;
    }
    public string ValueText{ 
        get => _valueText.text;
        set => _valueText.text = value;
    }

    // Start is called before the first frame update
    void Awake()
    {
        _labelText = transform.Find("Label").GetComponent<TextMeshProUGUI>();
        _valueText = transform.Find("Value").GetComponent<TextMeshProUGUI>();
    }

    public void Reset(){
        _labelText.text = "";
        _valueText.text = "";
    }
}
