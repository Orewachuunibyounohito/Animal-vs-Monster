using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class NewLibrary<T> where T : InstantiateData
{
    #region Field
    private Dictionary<string, T> allDatas = new Dictionary<string, T>();
    #endregion

    #region Property
    public  Dictionary<string, T> dataDictionary        => allDatas;
    public  T                     this[string dataName] => allDatas[dataName]; 
    #endregion

    #region Constructor
    public NewLibrary(){ allDatas = new Dictionary<string, T>(); }
    public NewLibrary( List<T> datas ){
        foreach( var data in datas ){
            allDatas.Add( data.dataName, data );
        }
    }
    public NewLibrary( T[] datas ){
        foreach( var data in datas ){
            allDatas.Add( data.dataName, data );
        }
    }
    #endregion

    public void Add(T data) => allDatas.Add( data.dataName, data );

    #region Help Function
    public void ShowDatas(){
        string log = "[";
        foreach( var data in dataDictionary ){ 
            log += $"{data.Key}: {data.Value}, ";
        }
        log = log.Substring( 0, log.Count()-2 )+"]";
        Debug.Log( log );
    }
    #endregion

}
