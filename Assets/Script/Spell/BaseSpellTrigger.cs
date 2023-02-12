using System.Collections.Generic;
using UnityEngine;

public abstract class BaseSpellTrigger
{
    public abstract void Trigger(SpellData spellData,SpellTiles spellTiles);
    public abstract void ComputeSpellData(BoardEntity entity);
}