using KarpysDev.Script.Manager;
using KarpysDev.Script.Map_Related;
using KarpysDev.Script.Map_Related.Blight;
using KarpysDev.Script.Widget;
using UnityEngine;

namespace KarpysDev.Script.Entities.EntitiesBehaviour
{
    public class BlightBehaviour:EntityBehaviour
    {
        private BlightSpawner m_BlightSpawner = null;
        private int m_PathId = 0;
        public BlightBehaviour(BlightSpawner blightSpawner)
        {
            m_BlightSpawner = blightSpawner;
        }

        protected override void InitializeEntityBehaviour()
        {
            GameManager.Instance.RegisterActiveEnemy(m_AttachedEntity);
            m_AttachedEntity.EntityEvent.OnDeath += ReduceBlightCount;
        }

        ~BlightBehaviour()
        {
            m_AttachedEntity.EntityEvent.OnDeath -= ReduceBlightCount;
        }

        private void ReduceBlightCount()
        {
            m_BlightSpawner.BlightCore.ReduceBlightCount();
        }

        public override void Behave()
        {
            BehaveLogic();
            m_AttachedEntity.EntityEvent.OnBehave?.Invoke();
        }

        private void BehaveLogic()
        {
            Tile nextTile = m_BlightSpawner.GetNextBranchPath(m_PathId);

            if (nextTile == null)
            {
                Debug.Log("Core Reached");
                return;
            }
        
            bool canMoveToNext = true;

            if (!nextTile.Walkable)
            {
                BoardEntity entityOnTile = MapData.Instance.GetEntityAt(nextTile.TilePosition, EntityGroup.Friendly);

                if (entityOnTile)
                {
                    KnockBackEntity(entityOnTile,out canMoveToNext);
                }
                else
                {
                    Debug.Log("Blight Path Block by unmovable block or enemy entity");
                    canMoveToNext = false;
                }
            }

            if (canMoveToNext)
            {
                m_AttachedEntity.MoveTo(nextTile.XPos, nextTile.YPos);
                m_PathId += 1;
            }
        }

        private void KnockBackEntity(BoardEntity entity,out bool canMoveToNext)
        {
            Tile tile = m_AttachedEntity.Map.GetTile(TileHelper.GetOppositePositionFrom(entity.EntityPosition, m_AttachedEntity.EntityPosition));

            if (tile.Walkable)
            {
                entity.MoveTo(tile.TilePosition);
                canMoveToNext = true;
            }
            else
            {
                canMoveToNext = false;
            }
        }
    }
}