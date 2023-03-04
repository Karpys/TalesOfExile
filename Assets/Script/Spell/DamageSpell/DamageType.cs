[System.Serializable]
public class DamageType
{
    public MainDamageType MainDamageType = MainDamageType.Melee;
    public SubDamageType[] SubDamageTypes = new SubDamageType[1];

    public DamageType(DamageType damageType)
    {
        MainDamageType = damageType.MainDamageType;
        SubDamageTypes = new SubDamageType[damageType.SubDamageTypes.Length];

        for (int i = 0; i < damageType.SubDamageTypes.Length; i++)
        {
            SubDamageTypes[i] = damageType.SubDamageTypes[i];
        }
    }
}