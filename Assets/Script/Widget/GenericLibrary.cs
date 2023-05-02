using System;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

[System.Serializable]
public class GenericLibrary<O,K> where O : class where K : struct
{
    [SerializeField] private LibraryKey<O, K>[] m_Keys = null;
    private Dictionary<K,O> m_Dictionary = new Dictionary<K,O>();

    public Dictionary<K, O> Dictionary => m_Dictionary;

    public void InitializeDictionary()
    {
        m_Dictionary.Clear();
        
        foreach (LibraryKey<O,K> key in m_Keys)
        {
            m_Dictionary.Add(key.Type,key.Object);
        }            
    }

    public void SetKeys(LibraryKey<O, K>[] libraryKey)
    {
        m_Keys = libraryKey;
    }
    
    public O GetViaKey(K type)
    {
        O obj = null;
        m_Dictionary.TryGetValue(type,out obj);

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

    public LibraryKey(O obj, K key)
    {
        Object = obj;
        Type = key;
    }
}
