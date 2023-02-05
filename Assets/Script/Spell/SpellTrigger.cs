
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseSpellTriggerScriptable : ScriptableObject
{
    //Damage Class Parameter//
    public abstract void Trigger(SpellData spellData,List<List<Vector2Int>> actionTiles);
}