namespace KarpysDev.Script.Spell
{
    public enum SpellGroup
    {
        //Physical Group 1 - 20//
        AutoAttack = 0,
        RangeAttack = 1,
        Physical = 2,
        
        //Elemental / Spell Group 21 -50//
        Spell = 21,
        Elemental = 22,
        Lightning = 23,
        Fire = 24,
        Cold = 25,
        Rock = 26,
        //Misc group 51-100//
        Projectile = 51,
        //Buff Group 101+//
        BuffToggle = 101,
        Buff = 102,
        Curse = 103,
        Debuff = 104,
        
    }

    public static class SpellGroupExtensions
    {
        public static string ToDescription(this SpellGroup spellGroup)
        {
            switch (spellGroup)
            {
                case SpellGroup.AutoAttack:
                    return "Attack";
                case SpellGroup.BuffToggle:
                    return "Toggle";
                case SpellGroup.RangeAttack:
                    return "Range Attack";
                default:
                    return spellGroup.ToString();
            }
        }
    }
}
