using System;
using UnityEngine;

public class EntityLibrary : SingletonMonoBehavior<EntityLibrary>
{
    [SerializeField] private GenericObjectLibrary<BoardEntity, EntityType> Library = null;

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

[System.Serializable]
public class EntityKey
{
    public BoardEntity Entity = null;
    public EntityType Type = EntityType.None;
}

public enum EntityType
{
    None,
    Balista,
}
