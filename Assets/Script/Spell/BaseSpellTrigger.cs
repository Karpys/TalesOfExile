using System.Collections.Generic;

public abstract class BaseSpellTrigger
{
    protected float m_SpellAnimDelay = 0;
    public abstract void Trigger(TriggerSpellData spellData,SpellTiles spellTiles);
    public abstract void ComputeSpellData(BoardEntity entity);
}