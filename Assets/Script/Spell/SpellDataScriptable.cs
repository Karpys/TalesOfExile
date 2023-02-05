using System;
using UnityEngine;


[CreateAssetMenu(fileName = "Spell", menuName = "New Spell", order = 0)]
public class SpellDataScriptable : ScriptableObject
{
    //FIX PARAMETERS//
    [Header("Spell UI")]
    public Sprite m_SpellIconBorder = null;
    public Sprite m_SpellIcon = null;

    [Header("Spell Trigger Options")]
    //Add spell type enum : Passif / Usable Spell(show only them in the spell bar) ect ect...
    public BaseSpellTriggerScriptable m_SpellTrigger = null;
    [Header("Spell Display In Game Option")]
    public ZoneSelection[] m_Selection = null;
    [Header("Base Spell Data")]
    public int Range = 0;
}

[System.Serializable]
public class SpellData
{
    public SpellDataScriptable m_Data = null;
    //Spell Variables//
    public BoardEntity AttachedEntity = null;
}

[System.Serializable]
public class ZoneSelection
{
    public ZoneOrigin Origin = ZoneOrigin.Self;
    public ZoneType DisplayType = ZoneType.Square;
    public int Range = 0;
    public ValidationType ValidationType = null;
    public bool ActionSelection = false;
}
[Serializable]
public class ValidationType
{
    public bool NeedValidation = false;
    public int TargetZoneValidation = -1;
}

public enum ZoneType
{
    Circle,
    Square,
}
public enum ZoneOrigin
{
    Self,
    Mouse,
}
