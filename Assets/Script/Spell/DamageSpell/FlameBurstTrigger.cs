using KarpysDev.Script.Entities;
using KarpysDev.Script.Manager.Library;
using UnityEngine;

namespace KarpysDev.Script.Spell.DamageSpell
{
    public class FlameBurstTrigger : DamageSpellTrigger
    {
        private int m_BurnDuration = 0;
        private float m_BurnValue = 0;

        public FlameBurstTrigger(DamageSpellScriptable damageSpellData, int burnDuration, float burnValue) : base(
            damageSpellData)
        {
            m_BurnDuration = burnDuration;
            m_BurnValue = burnValue;
        }

        protected override void EntityHit(BoardEntity entity, TriggerSpellData spellData, Vector2Int origin, CastInfo castInfo)
        {
            base.EntityHit(entity, spellData, origin, castInfo);
            BuffLibrary.Instance.AddBuffToViaKey(BuffType.BurnDotDebuff, entity).InitializeAsBuff(m_AttachedSpell.AttachedEntity,entity,m_BurnDuration,m_BurnValue);
        }
    }
}