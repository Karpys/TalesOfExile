using KarpysDev.Script.Entities;
using KarpysDev.Script.Manager.Library;
using KarpysDev.Script.Widget;
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
            entity.GiveBuff(BuffType.BurnDotDebuff, m_BurnDuration, m_BurnValue, m_AttachedSpell.AttachedEntity);
        }
    }
}