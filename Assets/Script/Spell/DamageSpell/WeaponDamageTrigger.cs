public class WeaponDamageTrigger : DamageSpellTrigger
{
    public float WeaponDamageConvertion = 100f;
    public WeaponDamageTrigger(WeaponDamageSpellScriptable weaponDamageData):base(weaponDamageData)
    {
        WeaponDamageConvertion = weaponDamageData.BaseWeaponDamageConvertion;
    }

    public override void ComputeSpellData(BoardEntity entity)
    {
        DamageSpellData.InitialSourceDamage.Damage = entity.GetMainWeaponDamage() / WeaponDamageConvertion * 100;
        base.ComputeSpellData(entity);
    }
}