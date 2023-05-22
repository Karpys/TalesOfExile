public enum ModifierType
{
    None = -1,
    //Damage Type Up// 0 => 20//
    UpFire = 0,
    UpCold = 1,
    UpPhysical = 2,
    UpLightning = 3,
    UpElemental = 4,
    IncreaseSpellDamage = 19,
    IncreaseWeaponForce = 20,
    //Defense Type// 21 => 50//
    IncreaseMaxLife = 21,
    UpFireResistance = 22,
    UpColdResistance = 23,
    UpPhysicalResistance = 24,
    UpLightningResistance = 25,
    UpElementResistance = 26,
    //Misc// 51 +//
    SpellAddition = 51,
    AddThrowRockPassif = 52,
    CanUseBowTalent = 53,
    //Ect
}


//For everything that need to sub/unsub to the attached event pass throught create a passive buff//
//Key system to know the buff that come from wich equipement, for the key use EquipementSaveName => Fix string//