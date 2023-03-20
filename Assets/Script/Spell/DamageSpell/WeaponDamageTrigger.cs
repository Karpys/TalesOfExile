public class WeaponDamageTrigger : DamageSpellTrigger
{
    public float WeaponDamageConvertion = 100f;
    public WeaponDamageTrigger(WeaponDamageSpellScriptable weaponDamageData):base(weaponDamageData)
    {
        WeaponDamageConvertion = weaponDamageData.BaseWeaponDamageConvertion;
    }
    protected override void ComputeSpellDamage(BoardEntity entity)
    {
        base.ComputeSpellDamage(entity);
        DamageSource weaponDamageSource = new DamageSource(entity.GetMainWeaponDamage() * WeaponDamageConvertion / 100,
            DamageSpellData.InitialSourceDamage.DamageType);
        AddDamageSource(weaponDamageSource);
    }
}