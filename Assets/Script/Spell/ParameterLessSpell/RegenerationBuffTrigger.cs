using UnityEngine;

public class RegenerationBuffTrigger : BuffGiverTrigger
{
    public RegenerationBuffTrigger(BaseSpellTriggerScriptable baseScriptable, BuffGroup buffGroup, BuffType buffType, int buffDuration, float buffValue) : base(baseScriptable, buffGroup, buffType, buffDuration, buffValue)
    {
    }
    
    public override void Trigger(TriggerSpellData spellData, SpellTiles spellTiles)
    {
        m_SpellAnimDelay = 0.1f;
        base.Trigger(spellData, spellTiles);
    }

    protected override int GetSpellPriority()
    {
        BoardEntity entity = m_AttachedSpell.AttachedEntity;
        if (entity.Life.Life < entity.Life.MaxLife)
            return 1;
        
        return 0;
    }

    public override void ComputeSpellPriority()
    {
        m_SpellPriority = 0;
    }

    
}