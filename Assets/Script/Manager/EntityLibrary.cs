using UnityEngine;

public class EntityLibrary : SingletonMonoBehavior<EntityLibrary>
{
    public EntityKey[] Library = null;
    
    public BoardEntity GetEntityViaKey(EntityType type)
    {
        foreach (EntityKey entityKey in Library)
        {
            if (entityKey.Type == type)
            {
                return entityKey.Entity;
            }
        }

        Debug.LogError("No Entity Type Found " + type);
        return null;
    }
}

[System.Serializable]
public class EntityKey
{
    public BoardEntity Entity = null;
    public EntityType Type = EntityType.Balista;
}

public enum EntityType
{
    None,
    Balista,
}
