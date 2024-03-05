using KarpysDev.Script.Entities;
using KarpysDev.Script.Manager.Library;
using KarpysDev.Script.Map_Related;
using KarpysDev.Script.Spell.SpellFx;
using KarpysDev.Script.Widget;
using UnityEngine;

namespace KarpysDev.Script.Spell.DamageSpell
{
    using Entities.BuffRelated;

    public class GrabChainTrigger : SelectionSpellTrigger
    {
        private int m_StunDuration = 0;
        public GrabChainTrigger(BaseSpellTriggerScriptable baseScriptable,int stunDuration) : base(baseScriptable)
        {
            m_StunDuration = stunDuration;
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
            entity.Buffs.AddBuff(new StunDebuff(m_AttachedSpell.AttachedEntity, entity, BuffType.StunDebuff,BuffGroup.Debuff,m_StunDuration,1),VisualEffectType.StunStars);
        }

        private Transform m_EntityHit = null;
        
        protected override SpellAnimation CreateOnHitFx(Vector3 entityPosition, Transform transform)
        {
            SpellAnimation spellAnim = base.CreateOnHitFx(entityPosition, transform);

            if (spellAnim is IPointAttach pointAttach)
            {
                pointAttach.CasterTransform = m_AttachedTransform;
                pointAttach.TargetTransform = m_EntityHit.transform;
                pointAttach.TargetPosition = m_TargetPosition;
            }
            else
            {
                Debug.LogError("Consider using a point attach spell animation");                
            }

            return spellAnim;
        }
        
    }
}