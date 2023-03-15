using System.Collections.Generic;

public abstract class BaseSpellTrigger
{
    protected float m_SpellAnimDelay = 0;
    protected int m_SpellPriority = 0;
    public int SpellPriority => m_SpellPriority;
    public abstract void ComputeSpellPriority();
    public abstract void Trigger(TriggerSpellData spellData,SpellTiles spellTiles);
    public abstract void ComputeSpellData(BoardEntity entity);
}