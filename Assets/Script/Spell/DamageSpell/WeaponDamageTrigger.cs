public class WeaponDamageTrigger : DamageSpellTrigger
{
    public float WeaponDamageConvertion = 100f;
    
    public WeaponDamageTrigger(DamageSpellScriptable damageSpellData,float baseWeaponDamageConvertion) : base(damageSpellData)
    {
        WeaponDamageConvertion = baseWeaponDamageConvertion;
    }
    
    protected override void ComputeSpellDamage(BoardEntity entity)
    {
        base.ComputeSpellDamage(entity);
        DamageSource weaponDamageSource = new DamageSource(entity.GetMainWeaponDamage() * WeaponDamageConvertion / 100,
            m_DamageSpellParams.InitialSourceDamage.DamageType);
        AddDamageSource(weaponDamageSource);
    }

    
}