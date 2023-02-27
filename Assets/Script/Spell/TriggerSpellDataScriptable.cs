using UnityEngine;


[CreateAssetMenu(fileName = "Spell", menuName = "New Trigger Spell", order = 0)]
public class TriggerSpellDataScriptable : SpellDataScriptable
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