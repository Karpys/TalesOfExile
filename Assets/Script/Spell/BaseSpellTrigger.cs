using System.Collections.Generic;

public abstract class BaseSpellTrigger
{
    protected float m_SpellAnimDelay = 0;
    protected int m_SpellPriority = 0;

    protected SpellData m_AttachedSpell = null;
    //Cooldown Part//
    public int SpellPriority => GetSpellPriority();
    protected virtual int GetSpellPriority()
    {
        return m_SpellPriority;
    }

    public void SetAttachedSpell(SpellData spellData)
    {
        m_AttachedSpell = spellData;
    }
    public abstract void ComputeSpellPriority();
    public abstract void Trigger(TriggerSpellData spellData,SpellTiles spellTiles);

    public abstract void ComputeSpellData(BoardEntity entity);
}