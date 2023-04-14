using System;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

[System.Serializable]
public class GenericObjectLibrary<O,K> where O : class where K : struct
{
    [SerializeField] private LibraryKey<O, K>[] m_Keys = null;
    private Dictionary<K,O> Keys = new Dictionary<K,O>();


    public void InitializeDictionary()
    {
        foreach (LibraryKey<O,K> key in m_Keys)
        {
            Keys.Add(key.Type,key.Object);
        }            
    }
    
    public O GetViaKey(K type)
    {
        O obj = null;
        Keys.TryGetValue(type,out obj);

        if (obj == null)
        {
            Debug.LogError("Type not found in library :"+type);
            return null;
        }
        
        return obj;
    }
}

[System.Serializable]
public class LibraryKey<O,K> where O : class where K : struct
{
    public O Object = null;
    public K Type;
}
