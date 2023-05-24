using UnityEngine;

namespace KarpysDev.Script.Items
{
    [CreateAssetMenu(menuName = "Inventory/WeaponItemData", fileName = "WeaponEquipementItemdata", order = 0)]
    public class WeaponEquipementItemdata : EquipementItemData
    {
        [Header("Weapon Specific")]
        [SerializeField] private bool m_IsTwoHanded = false;

        public bool TwoHanded => m_IsTwoHanded;
    }
}