﻿using System.Collections.Generic;
using KarpysDev.Script.Entities.EntitiesBehaviour;
using KarpysDev.Script.Manager.Library;
using KarpysDev.Script.Map_Related;
using KarpysDev.Script.PathFinding;
using KarpysDev.Script.Widget;
using UnityEngine;

namespace KarpysDev.Script.Entities.BuffRelated
{
    public class SpawnSkeletonCurse : Buff
    {
        private int m_SkeletonCount = 0;
        protected override void Apply()
        {
            m_Receiver.EntityEvent.OnDeath += SpawnSkeleton;
        }
    
        protected override void UnApply()
        {
            m_Receiver.EntityEvent.OnDeath -= SpawnSkeleton;
        }

        public override void InitializeAsBuff(BoardEntity caster, BoardEntity receiver, int cooldown, float buffValue, object[] args = null)
        {
            m_SkeletonCount = (int)args[0];
            base.InitializeAsBuff(caster, receiver, cooldown, buffValue, args);
        }

        private void SpawnSkeleton()
        {
            List<Tile> freeTile = TileHelper.GetNeighboursWalkable(MapData.Instance.Map.Tiles[m_Receiver.EntityPosition.x, m_Receiver.EntityPosition.y], NeighbourType.Square, MapData.Instance);
            freeTile.Add(MapData.Instance.GetTile(m_Receiver.EntityPosition));

            for (int i = 0; i < m_SkeletonCount; i++)
            {
                if(freeTile.Count <= 0)
                    return;
            
                int targetTile = Random.Range(0, freeTile.Count);
                Vector2Int spawnPosition = freeTile[targetTile].TilePosition;
            
                BoardEntity entity = EntityHelper.SpawnEntityOnMap(spawnPosition,EntityLibrary.Instance.GetEntityViaKey(EntityType.Skeleton),new BaseEntityIA(),m_Caster.EntityGroup);
                entity.gameObject.AddComponent<EntityLifeTurn>().SetTurnCount((int)m_BuffValue);
                entity.GetComponent<SummonTransmitter>().InitTransmitter(m_Caster);
                freeTile.RemoveAt(targetTile);
            }
        
        }

        public override object[] GetArgs()
        {
            object[] args = new object[1];
            args[0] = m_SkeletonCount;
            return args;
        }
    }
}