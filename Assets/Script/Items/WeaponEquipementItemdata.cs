using UnityEngine;

namespace KarpysDev.Script.Items
{
    [CreateAssetMenu(menuName = "Inventory/WeaponItemData", fileName = "WeaponEquipementItemdata", order = 0)]
    public class WeaponEquipementItemdata : EquipementItemData
    {
        [Header("Weapon Specific")]
        [SerializeField] private bool m_IsTwoHanded = false;

        [SerializeField] private WeaponType m_WeaponType = WeaponType.Sword;

        public bool TwoHanded => m_IsTwoHanded;
        public WeaponType WeaponType => m_WeaponType;
    }

    public enum WeaponType
    {
        Sword,
        Bow,
        Wand,
    }
}