namespace KarpysDev.Script.Entities.BuffRelated
{
    public enum BuffType
    {
        None = -1,
        //Buff type// 0 => 100//
        ModifierBuff = 0,
        RegenerationBuff = 1,
        RockThrowBuff = 2,
        FireHandBuff = 3,
        OnKillFlameBurst = 4,
        IcePrisonBuff = 5,
        HolyAttack = 6,
        //Debuff// 101 => 200//
        SilenceDebuff = 101,
        BurnDotDebuff = 102,
        StunDebuff = 103,
        RootDebuff = 104,
        //Mark / Misc// 201 +//
        SkeletonCurse = 201,
    }
}