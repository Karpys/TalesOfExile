using KarpysDev.Script.Entities;
using KarpysDev.Script.Manager;
using UnityEngine;

namespace KarpysDev.Script.Spell
{
    public class HealSelectionSpellTrigger : SelectionSpellTrigger
    {
        private float m_HealValue = 0;
        public HealSelectionSpellTrigger(BaseSpellTriggerScriptable baseScriptable, float healValue) : base(
            baseScriptable)
        {
            m_HealValue = healValue;
        }

        protected override void EntityHit(BoardEntity entity, TriggerSpellData spellData, Vector2Int spellOrigin, CastInfo castInfo)
        {
            base.EntityHit(entity, spellData, spellOrigin, castInfo);
            DamageManager.HealTarget(entity, m_HealValue, true, m_SpellAnimDelay);
        }

        protected override EntityGroup GetEntityGroup(TriggerSpellData spellData)
        {
            return spellData.AttachedEntity.EntityGroup;
        }


        public void SetHealValue(float healValue)
        {
            m_HealValue = healValue;
        }
    }
}