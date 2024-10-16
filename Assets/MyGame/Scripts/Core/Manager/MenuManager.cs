using UnityEngine;

public class MenuManager : MonoBehaviour
{
    #region Public Methods
    public void GameExit(){
        #if UNITY_EDITOR
        // If in UnityEditor, exiting play mode have to use
        // UnityEditor.EditorApplication.isPlaying = false;
        UnityEditor.EditorApplication.isPlaying = false;
        #else
        Application.Quit();
        #endif
    }

    public void EnterWorldMap(){
        GameManager.Instance.GameStart();
    }
    #endregion
}
