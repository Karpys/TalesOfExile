using KarpysDev.Script.Entities;
using UnityEngine;

namespace KarpysDev.Script.Spell.DamageSpell
{
    public class IceSpearTrigger : ProjectileDamageTrigger
    {
        private float m_AdditionalEfficiency = 0;
        public IceSpearTrigger(DamageSpellScriptable damageSpellData,OriginType originType,float effiencyAdditionalOnFirstHit) : base(damageSpellData,originType)
        {
            m_AdditionalEfficiency = effiencyAdditionalOnFirstHit;
        }

        private Vector2Int m_EndPosition = Vector2Int.zero;
        private float m_MaxEfficiency = 1;
        protected override void Trigger(TriggerSpellData spellData, SpellTiles spellTiles, CastInfo castInfo, float efficiency = 1)
        {
            m_MaxEfficiency = efficiency + m_AdditionalEfficiency;
            m_EndPosition = spellTiles.Last;
            base.Trigger(spellData, spellTiles, castInfo, efficiency);
            base.TileHit(m_EndPosition,spellData);
        }

        protected override void EntityHit(BoardEntity entity, TriggerSpellData spellData, Vector2Int origin, CastInfo castInfo)
        {
            base.EntityHit(entity, spellData, origin, castInfo);
            
            if (m_SpellEfficiency < m_MaxEfficiency)
            {
                m_SpellEfficiency += m_AdditionalEfficiency;
                m_SpellEfficiency = Mathf.Min(m_MaxEfficiency, m_SpellEfficiency);
            }
        }

        protected override void TileHit(Vector2Int tilePosition, TriggerSpellData spellData)
        {
            return;
        }
    }
}