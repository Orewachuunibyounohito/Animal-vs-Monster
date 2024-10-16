using UnityEngine;
using UnityEngine.Localization;

public class LocalizationPresenter : MonoBehaviour
{
    public LocalizationConfig config;

    private void Awake(){
        config = new LocalizationConfig();
    }
}
