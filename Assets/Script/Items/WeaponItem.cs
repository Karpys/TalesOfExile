namespace KarpysDev.Script.Items
{
    using Spell;
    using Utils;

    public class WeaponItem : EquipementItem
    {
        private WeaponType m_WeaponType = WeaponType.Sword;
        private SubDamageType m_WeaponDamageType = SubDamageType.Physical;

        public WeaponType WeaponType => m_WeaponType;
        public SubDamageType WeaponDamageType => m_WeaponDamageType;
        public WeaponItem(WeaponEquipementItemdata data) : base(data)
        {
            m_WeaponType = data.WeaponType;
        }

        public WeaponItem(string[] saveArgs) : base(saveArgs)
        {
            string weaponType = saveArgs[5];
            string weaponDamageType = saveArgs[6];
            m_WeaponType = (WeaponType) weaponType.ToInt();
            m_WeaponDamageType = (SubDamageType) weaponDamageType.ToInt();
        }

        public override string GetSaveData()
        {
            string baseSaveData = base.GetSaveData();
            baseSaveData += " " + (int)m_WeaponType;
            baseSaveData += " " + (int)m_WeaponDamageType;
            return baseSaveData;
        }
    }
}