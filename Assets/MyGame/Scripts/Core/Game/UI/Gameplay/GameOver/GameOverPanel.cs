using UnityEngine;
using UnityEngine.UI;

public class GameOverPanel : MonoBehaviour
{
    public Button Retry{ get; private set; }
    public Button BackToWorldMap{ get; private set; }

    void Awake(){
        Retry          = transform.Find("Buttons/Retry").GetComponent<Button>();
        BackToWorldMap = transform.Find("Buttons/Back").GetComponent<Button>();
    }
}