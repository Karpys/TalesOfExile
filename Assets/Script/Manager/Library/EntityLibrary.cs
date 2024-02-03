using KarpysDev.Script.Entities;
using UnityEngine;

namespace KarpysDev.Script.Manager.Library
{
    using KarpysUtils;

    public class EntityLibrary : SingletonMonoBehavior<EntityLibrary>
    {
        [SerializeField] private Widget.GenericLibrary<BoardEntity, EntityType> Library = null;

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
        None = 0,
        Balista = 1,
        Skeleton = 2,
        Labouk = 3,
        //Misc 101...
        Mine = 101,
        LaboukSpawner = 102,
    }
}