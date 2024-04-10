using KarpysDev.Script.Entities;
using KarpysDev.Script.Entities.BuffRelated;
using KarpysDev.Script.Manager.Library;

namespace KarpysDev.Script.Spell.ParameterLessSpell
{
    public class PsionicBuffGiver : BuffGiverTrigger
    {
        private int m_HitCount = 0;
        private ZoneType m_ZoneType = ZoneType.Circle;
        private int m_Range = 0;
        public PsionicBuffGiver(BaseSpellTriggerScriptable baseScriptable, BuffGroup buffGroup, BuffType buffType, BuffCooldown buffCooldown, int buffDuration, float buffValue, VisualEffectType visualEffectType,int hitCount,ZoneType zoneType,int range) : base(baseScriptable, buffGroup, buffType, buffCooldown, buffDuration, buffValue, visualEffectType)
        {
            m_HitCount = hitCount;
            m_ZoneType = zoneType;
            m_Range = range;
        }

        protected override Buff BuffToAdd(BoardEntity caster, BoardEntity receiver)
        {
            return new PsionicHitBuff(caster, receiver, m_BuffType, m_BuffGroup, m_BuffDuration, m_BuffValue,
                BuffConst.NotImplemented, false,
                BuffLibrary.Instance.UnarmedPsionicHitSpellInfo
                , m_HitCount, m_ZoneType, m_Range);
        }
    }
}