[System.Serializable]
public class DamageType
{
    public MainDamageType MainDamageType = MainDamageType.Melee;
    public SubDamageType[] SubDamageTypes = new SubDamageType[1];
}