using System.Runtime.CompilerServices;
using UnityEditor;
using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : Component
{
    private static T _instance;
    public  static T Instance{
        get{
            if( _instance == null ){
                _instance = FindObjectOfType<T>( true );
                if( _instance == null ){
                    var objName = typeof(T).ToString();
                    var gameObj = new GameObject(objName);
                    _instance   = gameObj.AddComponent<T>();
                }
            }
            return _instance;
        }
    }

    protected virtual void Awake()
    {
        DuplicateRemove();
        DontDestroyOnLoad( gameObject );
    }

    private void DuplicateRemove()
    {
        if (_instance == null) { _instance = GetComponent<T>(); }
        else                   { Destroy(gameObject); }
    }
}
