using KarpysDev.Script.Entities;
using KarpysDev.Script.Map_Related;
using KarpysDev.Script.Widget;
using UnityEngine;

namespace KarpysDev.Script.Spell.DamageSpell
{
    public class GrabChainTrigger : SelectionSpellTrigger
    {
        public GrabChainTrigger(BaseSpellTriggerScriptable baseScriptable) : base(baseScriptable)
        {
        }
        
        private Transform m_AttachedTransform = null;
        private Vector3 m_TargetPosition = Vector3.zero;

        protected override void Trigger(TriggerSpellData spellData, SpellTiles spellTiles, CastInfo castInfo, float efficiency = 1)
        {
            m_AttachedTransform = m_AttachedSpell.AttachedEntity.transform;
            base.Trigger(spellData, spellTiles, castInfo, efficiency);
        }

        protected override void EntityHit(BoardEntity entity, TriggerSpellData spellData, Vector2Int origin, CastInfo castInfo)
        {
            m_EntityHit = entity.transform;
            Tile closestFree = TileHelper.GetFreeClosestAround(MapData.Instance.GetTile(spellData.AttachedEntity.EntityPosition),entity.WorldPosition);

            if (closestFree != null)
            {
                entity.MoveTo(closestFree.TilePosition,false);
                m_TargetPosition = MapData.Instance.GetTilePosition(closestFree.TilePosition);
            }
            else
            {
                m_TargetPosition = MapData.Instance.GetTilePosition(entity.EntityPosition);
            }
            
            base.EntityHit(entity, spellData, origin, castInfo);
        }

        private Transform m_EntityHit = null;
        
        protected override void TriggerOnHitFx(Vector3 entityPosition, Transform transform, params object[] args)
        {
            base.TriggerOnHitFx(entityPosition, transform, m_EntityHit.transform,m_AttachedTransform,m_TargetPosition);
        }
        
    }
}