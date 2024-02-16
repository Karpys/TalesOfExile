using UnityEngine;

namespace KarpysDev.Script.Items
{
    using Spell;

    [CreateAssetMenu(menuName = "Inventory/WeaponItemData", fileName = "WeaponEquipementItemdata", order = 0)]
    public class WeaponEquipementItemdata : EquipementItemData
    {
        [Header("Weapon Specific")]
        [SerializeField] private bool m_IsTwoHanded = false;
        [SerializeField] private WeaponType m_WeaponType = WeaponType.Sword;
        [SerializeField] private SubDamageType m_WeaponDamageType = SubDamageType.Physical;
        [SerializeField] private float m_WeaponBaseFlatDamage = 0;

        public bool TwoHanded => m_IsTwoHanded;
        public WeaponType WeaponType => m_WeaponType;
        public SubDamageType WeaponDamageType => m_WeaponDamageType;
        public float WeaponBaseFlatDamage => m_WeaponBaseFlatDamage;
    }

    public enum WeaponType
    {
        Sword,
        Bow,
        Wand,
    }
}