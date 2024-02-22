namespace KarpysDev.Script.Items
{
    using Entities;
    using KarpysUtils;
    using Spell;
    using Spell.DamageSpell;
    using StringUtils = Utils.StringUtils;

    public class WeaponItem : EquipementItem,IWeapon
    {
        private WeaponType m_WeaponType = WeaponType.Sword;
        private SubDamageType m_WeaponDamageType = SubDamageType.Physical;
        private float m_WeaponFlatDamage = 0;

        public WeaponType WeaponType => m_WeaponType;
        public SubDamageType WeaponDamageType => m_WeaponDamageType;
        public float WeaponFlatDamage => m_WeaponFlatDamage;
        public WeaponItem(WeaponEquipementItemdata data) : base(data)
        {
            m_WeaponType = data.WeaponType;
            m_WeaponDamageType = data.WeaponDamageType;
            m_WeaponFlatDamage = data.WeaponBaseFlatDamage;
        }

        public WeaponItem(string[] saveArgs) : base(saveArgs)
        {
            string weaponType = saveArgs[5];
            string weaponDamageType = saveArgs[6];
            string weaponFlatDamage = saveArgs[7];
            m_WeaponType = (WeaponType) StringUtils.ToInt(weaponType);
            m_WeaponDamageType = (SubDamageType) StringUtils.ToInt(weaponDamageType);
            m_WeaponFlatDamage = StringUtils.ToFloat(weaponFlatDamage);

            CustomLog.Log(Data.ObjectName,"Create from save");
        }

        public override string GetSaveData()
        {
            string baseSaveData = base.GetSaveData();
            baseSaveData += " " + (int)m_WeaponType;
            baseSaveData += " " + (int)m_WeaponDamageType;
            baseSaveData += " " + m_WeaponFlatDamage;
            return baseSaveData;
        }

        private float GetFlatDamage()
        {
            return m_WeaponFlatDamage;
        }

        public DamageSource GetWeaponDamage(BoardEntity entity)
        {
            //Todo:Get Damage WeaponType Modifier => SwordDamageModifier//
            return new DamageSource(GetFlatDamage(), m_WeaponDamageType);
        }

        public void EquipWeapon(BoardEntity entity,WeaponTarget target)
        {
            entity.EntityStats.AddWeapon(this,target);
            Equip(entity);
        }

        public void UnEquipWeapon(BoardEntity entity, WeaponTarget target)
        {
            entity.EntityStats.RemoveWeapon(this,target);
            UnEquip(entity);
        }
    }

    public interface IWeapon
    {
        public DamageSource GetWeaponDamage(BoardEntity entity);
        public WeaponType WeaponType { get; }
    }
}