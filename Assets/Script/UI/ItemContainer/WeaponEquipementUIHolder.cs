using System.Collections.Generic;
using KarpysDev.Script.Items;
using UnityEngine;

namespace KarpysDev.Script.UI.ItemContainer
{
    public class WeaponEquipementUIHolder : EquipementItemUIHolder
    {
        [SerializeField] private bool m_IsMainEquipement = false;
        [SerializeField] private WeaponEquipementUIHolder m_OtherEquipementHolder = null;

        private bool IsTwoHanded => Item != null && ((WeaponEquipementItemdata) Item.Data).TwoHanded;
    
        public WeaponEquipementUIHolder GetMain()
        {
            if (m_IsMainEquipement)
                return this;
            return m_OtherEquipementHolder;
        }
    
        public WeaponEquipementUIHolder GetSub()
        {
            if (!m_IsMainEquipement)
                return this;
            return m_OtherEquipementHolder;
        }

        public bool IsTwoHandedCurrentlyEquiped()
        {
            if (IsTwoHanded || m_OtherEquipementHolder.IsTwoHanded)
                return true;
            return false;
        }

        public EquipementItemUIHolder[] GetWeaponEquiped()
        {
            List<EquipementItemUIHolder> holders = new List<EquipementItemUIHolder>();
        
            if (Item != null)
                holders.Add(this);
            if (m_OtherEquipementHolder.Item != null)
                holders.Add(m_OtherEquipementHolder);
            return holders.ToArray();
        }
    }
}