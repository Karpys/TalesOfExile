using System;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "Spell", menuName = "Trigger/A_TriggerSpell", order = 0)]
public class TriggerSpellDataScriptable : SpellDataScriptable
{
    [Header("Trigger Spell Data")]
    //FIX PARAMETERS//
    [Header("Spell UI")]
    public Sprite m_SpellIconBorder = null;
    public Sprite m_SpellIcon = null;

    [Header("Spell Trigger Options")]
    //Add spell type enum : Passif / Usable Spell(show only them in the spell bar) ect ect...
    public BaseSpellTriggerScriptable m_SpellTrigger = null;
    public int m_BaseCooldown = 0;
    
    [Header("Spell Display In Game Option")]
    public ZoneSelection[] m_Selection = null;
    [SerializeField] private int m_MainSelection = 0;
    public int MainSelection => m_MainSelection;
    [Header("Spell Restriction")]
    public List<SpellRestriction> SpellRestrictions = new List<SpellRestriction>();

    [Header("Spell Rules / Used By Enemy")]
    public Zone AllowedCastZone = null;
    public SpellOriginType OriginSelection = SpellOriginType.ClosestEnemy;
}

[System.Serializable]
public class SpellRestriction
{
    public int SelectionId = 0;
    public SpellRestrictionType Type = SpellRestrictionType.OriginOnWalkable;
}

public enum SpellRestrictionType
{
    OriginOnWalkable,
    FreeTileAroundEnemyTarget,
    IsBowUser,
}

public enum SpellOriginType
{
    //Closest Enemy default//
    ClosestEnemy,
    ClosestAlly,
    RandomWalkableInsideMainSelection,
    ClosestInverseAtDistance,
    Self,
}