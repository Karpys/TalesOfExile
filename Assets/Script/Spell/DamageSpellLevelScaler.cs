namespace KarpysDev.Script.Spell
{
    using System;
    using DamageSpell;

    public class DamageSpellLevelScaler : ILevelScaler
    {
        private DamageSpellLevelScalerScriptable m_LevelScalerData = null;
        private float m_LastDamageAmplifier = 0f;
        public DamageSpellLevelScaler(DamageSpellLevelScalerScriptable damageSpellLevelScalerScriptable)
        {
            m_LevelScalerData = damageSpellLevelScalerScriptable;
        }

        public void Apply(TriggerSpellData triggerSpellData)
        {
            if (triggerSpellData.SpellTrigger is IDamageProvider spellDamage)
            {
                float damageAmplifier = m_LevelScalerData.Evaluate(triggerSpellData.LevelRatio);
                if (Math.Abs(damageAmplifier - m_LastDamageAmplifier) > 0.05f)
                {
                    RemoveDamageModifier(spellDamage,m_LastDamageAmplifier);
                    m_LastDamageAmplifier = damageAmplifier;
                    ApplyDamageModifier(spellDamage,damageAmplifier);
                }
            }
        }

        private void ApplyDamageModifier(IDamageProvider spellDamage,float damageAmp)
        {
            foreach (DamageSource damageSource in spellDamage.DamageSources)
            {
                damageSource.AmplifyBy(damageAmp);
            }
        }

        private void RemoveDamageModifier(IDamageProvider spellDamage,float oldDamageAmp)
        {
            foreach (DamageSource damageSource in spellDamage.DamageSources)
            {
                damageSource.AmplifyBy(-oldDamageAmp);
            }
        }
    }
}