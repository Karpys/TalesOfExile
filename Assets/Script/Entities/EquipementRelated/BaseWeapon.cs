namespace KarpysDev.Script.Entities.EquipementRelated
{
    using Items;
    using Spell.DamageSpell;

    public class BaseWeapon : IWeapon
    {
        private DamageSource m_WeaponDamage = null;
        private WeaponType m_WeaponType = WeaponType.Sword;
        
        public WeaponType WeaponType => m_WeaponType;

        public BaseWeapon(DamageSource weaponDamage, WeaponType weaponType)
        {
            m_WeaponDamage = weaponDamage;
            m_WeaponType = weaponType;
        }
        public DamageSource GetWeaponDamage(BoardEntity entity)
        {
            return new DamageSource(m_WeaponDamage);
        }
    }
}