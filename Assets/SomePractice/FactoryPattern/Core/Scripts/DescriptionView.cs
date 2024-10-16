using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;

public class DescriptionView : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI descriptionText;

    private void Awake(){
        // descriptionText = GetComponent<TextMeshProUGUI>();
    }

    public void UpdateDescription(string description) => descriptionText.SetText(description);

    [Button("Hamburger")]
    public void ShowHamburger() => UpdateDescription(new Hamburger().Info());
    [Button("BigMac")]
    public void ShowBigMac() => UpdateDescription(new BigMac().Info());
    [Button("McChicken")]
    public void ShowMcChicken() => UpdateDescription(new McChicken().Info());
    [Button("Filet-O-Fish")]
    public void ShowFiletOFish() => UpdateDescription(new FiletOFish().Info());
}
