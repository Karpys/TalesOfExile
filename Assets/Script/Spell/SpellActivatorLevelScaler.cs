namespace KarpysDev.Script.Spell
{
    public class SpellActivatorLevelScaler : ILevelScaler
    {
        private SpellActivatorLevelScalerScriptable m_BaseData = null;
        
        public SpellActivatorLevelScaler(SpellActivatorLevelScalerScriptable baseData)
        {
            m_BaseData = baseData;
        }
        
        public void Apply(TriggerSpellData triggerSpellData)
        {
            bool isActive = m_BaseData.IsActive(triggerSpellData.LevelRatio);

            if (triggerSpellData.SpellTrigger is IActivator spellActivator)
            {
                if(isActive)
                    spellActivator.Enable();
                else
                    spellActivator.Disable();
            }
        }
    }

    public interface IActivator
    {
        public void Enable();
        public void Disable();
    }
}