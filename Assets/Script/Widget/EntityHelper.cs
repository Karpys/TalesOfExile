using System.Collections.Generic;
using System.Linq;
using KarpysDev.Script.Entities;
using KarpysDev.Script.Entities.EntitiesBehaviour;
using KarpysDev.Script.Map_Related;
using KarpysDev.Script.Map_Related.MapGeneration;
using KarpysDev.Script.Utils;
using UnityEngine;

namespace KarpysDev.Script.Widget
{
    public static class EntityHelper
    {
        public static EntityGroup GetInverseEntityGroup(EntityGroup entityGroup)
        {
            if (entityGroup == EntityGroup.Enemy)
                return EntityGroup.Friendly;
            if (entityGroup == EntityGroup.Friendly)
                return EntityGroup.Enemy;
            return EntityGroup.Neutral;
        }

        public static BoardEntity SpawnEntityOnMap(Vector2Int pos,BoardEntity entityPrefabBase,EntityBehaviour entityIa,EntityGroup entityGroup,EntityGroup targetEntityGroup = EntityGroup.None)
        {
            MapData mapData = MapData.Instance;
        
            BoardEntity boardEntity = GameObject.Instantiate(entityPrefabBase,mapData.transform);
            boardEntity.EntityInitialization(entityIa,entityGroup,targetEntityGroup);
            boardEntity.Place(pos.x,pos.y,mapData);
            return boardEntity;
        }

        public static void SpawnEnemyViaEntitySpawn(EntitySpawn[] entitySpawn)
        {
            foreach (EntitySpawn spawn in entitySpawn)
            {
                SpawnEntityOnMap(spawn.EntityPosition, spawn.EntityPrefab, new MapEnemyEntityBehaviour(), EntityGroup.Enemy,
                    EntityGroup.Friendly);
            }
        }
    
        public static BoardEntity GetClosestEntity(List<BoardEntity> entities,Vector2Int originPosition)
        {
            return entities.Where(en => en.Targetable).OrderBy(e => DistanceUtils.GetSquareDistance(originPosition, e.EntityPosition)).FirstOrDefault();
        }

        public static BehaveTiming GetBehaveTiming(BoardEntity entity, BehaveTiming timing)
        {
            if (timing == BehaveTiming.SameAsSource)
            {
                if (entity.EntityGroup == EntityGroup.Enemy)
                    return BehaveTiming.Enemy;
                if (entity.EntityGroup == EntityGroup.Friendly)
                    return BehaveTiming.Friendly;
            }

            return timing;
        }

        public static bool CanEditSpell(BoardEntity instanceControlledEntity)
        {
            if (instanceControlledEntity is ISpellSet set)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
