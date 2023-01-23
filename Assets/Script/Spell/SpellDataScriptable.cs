using UnityEngine;


[CreateAssetMenu(fileName = "Spell", menuName = "New Spell", order = 0)]
public class SpellDataScriptable : ScriptableObject
{
    public SpellOrigin Origin = SpellOrigin.Hero;
    public SpellDisplayType DisplayType = SpellDisplayType.Square;
    public int Range = 0;
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
