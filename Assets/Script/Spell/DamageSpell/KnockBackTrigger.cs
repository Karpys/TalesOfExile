using System.Collections.Generic;
using KarpysDev.Script.Entities;
using KarpysDev.Script.Map_Related;
using KarpysDev.Script.Widget;
using TweenCustom;
using UnityEngine;

namespace KarpysDev.Script.Spell.DamageSpell
{
    public class KnockBackTrigger : DamageSpellTrigger
    {
        private List<BoardEntity> m_EntityHits = new List<BoardEntity>();
        private int m_RepulseForce = 2;

        private TriggerSpellData m_RangeAutoTrigger = null;
    
        public KnockBackTrigger(DamageSpellScriptable damageSpellData, int repulseForce) : base(damageSpellData)
        {
            m_RepulseForce = repulseForce;
        }

        protected override void Trigger(TriggerSpellData spellData, SpellTiles spellTiles,CastInfo castInfo, float efficiency = 1)
        {
            m_EntityHits.Clear();
            m_RangeAutoTrigger = spellData.AttachedEntity.GetSpellViaKey("Range Attack");
            base.Trigger(spellData, spellTiles,castInfo,efficiency);
        }

        protected override void EntityHit(BoardEntity entity, TriggerSpellData spellData,
            Vector2Int origin, CastInfo castInfo)
        {
            if (m_EntityHits.Contains(entity))
            {
                UpdateEntityPosition(entity);   
                return;
            }
        
            m_EntityHits.Add(entity);

            for (int i = 0; i < m_RepulseForce; i++)
            {
                Vector2Int opposite = TileHelper.GetOppositePositionFrom(entity.EntityPosition, spellData.AttachedEntity.EntityPosition);
            
            
                if (MapData.Instance.IsWalkable(opposite))
                {
                    entity.MoveTo(opposite,false);
                    m_SpellAnimDelay = 0.1f;
                }
                else
                {
                    base.EntityHit(entity, spellData,origin,castInfo);
                    break;
                }
            }
        
            if(m_RangeAutoTrigger != null)
                SpellCastUtils.CastSpellAt(m_RangeAutoTrigger,entity.EntityPosition,spellData.AttachedEntity.EntityPosition,m_SpellEfficiency,true);
        
            UpdateEntityPosition(entity);
        }

        private void UpdateEntityPosition(BoardEntity entity)
        {
            entity.transform.DoMove(MapData.Instance.GetTilePosition(entity.EntityPosition), 0.1f);
        }
        
    }
}