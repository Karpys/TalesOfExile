﻿using KarpysDev.Script.Entities;
using KarpysDev.Script.Entities.BuffRelated;
using KarpysDev.Script.Manager.Library;

namespace KarpysDev.Script.Spell.ParameterLessSpell
{
    public class RegenerationBuffTrigger : BuffGiverTrigger
    {
        public RegenerationBuffTrigger(BaseSpellTriggerScriptable baseScriptable, BuffGroup buffGroup, BuffType buffType, int buffDuration, float buffValue) : base(baseScriptable, buffGroup, buffType, buffDuration, buffValue)
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