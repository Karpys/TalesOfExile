using KarpysDev.Script.Entities;

namespace KarpysDev.Script.Spell.DamageSpell
{
    public class TargetAllDamageSpellTrigger : DamageSpellTrigger
    {
        public TargetAllDamageSpellTrigger(DamageSpellScriptable damageSpellData) : base(damageSpellData)
        {}

        protected override EntityGroup GetEntityGroup(TriggerSpellData spellData)
        {
            return EntityGroup.All;
        }
    }
}