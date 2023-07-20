using System.Collections.Generic;
using KarpysDev.Script.Items;
using KarpysDev.Script.Manager.Library;
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
        public void UpdateFadeVisual()
        {
            WeaponEquipementUIHolder main = GetMain();
            WeaponEquipementUIHolder sub = GetSub();
            
            if (main.IsTwoHanded)
            {
                sub.m_ItemVisual.sprite = main.m_ItemVisual.sprite;
                sub.m_ItemVisual.color = new Color(1,1,1,0.5f);
                sub.SetBorder(RarityLibrary.Instance.GetParametersViaKey(main.Item.Rarity).RarityColor,0.5f);
            }
            else if(sub.Item == null)
            {
                sub.DefaultDisplay();
            }
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