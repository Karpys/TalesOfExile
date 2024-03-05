using KarpysDev.Script.Entities;
using KarpysDev.Script.Entities.BuffRelated;
using KarpysDev.Script.Manager.Library;

namespace KarpysDev.Script.Spell.ParameterLessSpell
{
    public class RegenerationBuffTrigger : BuffGiverTrigger
    {
        public RegenerationBuffTrigger(BaseSpellTriggerScriptable baseScriptable, BuffGroup buffGroup,BuffType buffType, BuffCooldown buffCooldown, int buffDuration, float buffValue,VisualEffectType visualEffectType) : base(baseScriptable, buffGroup,buffType, buffCooldown, buffDuration, buffValue,visualEffectType)
        {
        }
        protected override int GetSpellPriority()
        {
            BoardEntity entity = m_AttachedSpell.AttachedEntity;
            if (entity.Life.Life < entity.Life.MaxLife)
                return m_SpellPriority;
        
            return 0;
        }
    }
}