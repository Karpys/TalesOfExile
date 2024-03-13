using System.Collections.Generic;
using System.Linq;
using KarpysDev.Script.Entities;
using KarpysDev.Script.Entities.BuffRelated;
using KarpysDev.Script.Entities.EntitiesBehaviour;
using KarpysDev.Script.Manager.Library;
using KarpysDev.Script.Map_Related;
using KarpysDev.Script.Map_Related.MapGeneration;
using KarpysDev.Script.Map_Related.QuestRelated;
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
        
        public static EntityBehaviour ToEntityIA(this EntityIAType entityIA)
        {
            switch (entityIA)
            {
                case EntityIAType.MonsterMapEntity:
                    return new MapEnemyEntityBehaviour();
                case EntityIAType.MissionEntity:
                    return new MissionManBehaviour();
                case EntityIAType.VendingEntity:
                    return new VendingManBehaviour();
                case EntityIAType.Dummy:
                    return new DummyBehaviour();
                default:
                    return new BaseEntityIA();
            }
        }

        public static BoardEntity SpawnEntityOnMap(Vector2Int pos, BoardEntity entityPrefabBase,
            EntityBehaviour entityIa, EntityGroup entityGroup, EntityGroup targetEntityGroup = EntityGroup.None, bool inheritQuestModifier = false)
        {
            MapData mapData = MapData.Instance;
        
            BoardEntity boardEntity = GameObject.Instantiate(entityPrefabBase,mapData.transform);
            boardEntity.Place(pos.x,pos.y,mapData);
            boardEntity.EntityInitialization(entityIa,entityGroup,targetEntityGroup);

            if (inheritQuestModifier)
            {
                QuestModifierManager.Instance.OnMapEntitySpawn?.Invoke(boardEntity);
            }
            return boardEntity;
        }

        public static void SpawnViaEntitySpawn(EntitySpawn[] entitySpawn)
        {
            foreach (EntitySpawn spawn in entitySpawn)
            {
                SpawnEntityOnMap(spawn.EntityPosition, spawn.EntityPrefab, spawn.IAType.ToEntityIA(), spawn.EntityGroup,
                    spawn.TargetGroup,spawn.ShouldInheritQuestModifier);
            }
        }
    
        public static BoardEntity GetClosestEntity(List<BoardEntity> entities,Vector2Int originPosition)
        {
            BoardEntity closest = null;
            int closestSquareDistance = 100;
            int entitiesCount = entities.Count;

            for (int i = 0; i < entitiesCount; i++)
            {
                int currentSquareDistance = DistanceUtils.GetSquareDistance(originPosition, entities[i].EntityPosition);
                if (entities[i].Targetable && closestSquareDistance > currentSquareDistance)
                {
                    closest = entities[i];
                    closestSquareDistance = currentSquareDistance;
                }
            }
            
            return closest;
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
