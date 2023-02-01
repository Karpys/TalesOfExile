using UnityEngine;


[CreateAssetMenu(fileName = "Spell", menuName = "New Spell", order = 0)]
public class SpellDataScriptable : ScriptableObject
{
    //FIX PARAMETERS//
    [Header("Spell UI")]
    public Sprite m_SpellIconBorder = null;
    public Sprite m_SpellIcon = null;
    [Header("Spell Display In Game Option")]
    public SpellOrigin Origin = SpellOrigin.Hero;
    public SpellDisplayType DisplayType = SpellDisplayType.Square;
    [Header("Base Spell Data")]
    public int Range = 0;
}

[System.Serializable]
public class SpellData
{
    public SpellDataScriptable m_Data = null;
}


public enum SpellDisplayType
{
    Circle,
    Square,
}
public enum SpellOrigin
{
    Hero,
    Mouse,
}
