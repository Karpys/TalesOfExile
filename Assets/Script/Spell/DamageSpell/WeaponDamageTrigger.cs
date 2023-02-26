public class WeaponDamageTrigger : DamageSpellTrigger
{
    private WeaponDamageSpellScriptable WeaponData = null;
    public WeaponDamageTrigger(WeaponDamageSpellScriptable weaponDamageData):base(weaponDamageData)
    {
        DamageSpellData = weaponDamageData;
        WeaponData = weaponDamageData;
    }

    public override void ComputeSpellData(BoardEntity entity)
    {
        DamageSpellData.DamageParameters.InitialSourceDamage.Damage = entity.GetMainWeaponDamage() / WeaponData.WeaponDamageConvertion * 100;
        base.ComputeSpellData(entity);
    }
}