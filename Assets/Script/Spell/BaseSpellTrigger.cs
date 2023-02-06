using System.Collections.Generic;
using UnityEngine;

public abstract class BaseSpellTrigger
{
    public abstract void Trigger(SpellData spellData,List<List<Vector2Int>> actionTiles);
    public abstract void ComputeSpellData(BoardEntity entity);
}