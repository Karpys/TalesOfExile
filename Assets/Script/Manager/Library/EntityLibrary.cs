using System;
using UnityEngine;

public class EntityLibrary : SingletonMonoBehavior<EntityLibrary>
{
    [SerializeField] private GenericLibrary<BoardEntity, EntityType> Library = null;

    private void Awake()
    {
        Library.InitializeDictionary();
    }

    public BoardEntity GetEntityViaKey(EntityType type)
    {
        BoardEntity entity =  Library.GetViaKey(type);
        
        if(entity == null)
            Debug.LogError("No Entity Type Found " + type);
        
        return entity;
    }
}

public enum EntityType
{
    None,
    Balista,
    Skeleton,
    //Misc 101...
    Mine = 101,
}
