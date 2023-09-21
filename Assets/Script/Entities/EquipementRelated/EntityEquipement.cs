using KarpysDev.Script.Items;
using KarpysDev.Script.UI.ItemContainer.V2;
using UnityEngine;

namespace KarpysDev.Script.Entities.EquipementRelated
{
    public class EntityEquipement : MonoBehaviour
    {
        private PlayerEquipementHolderV2[] m_Equipement = new PlayerEquipementHolderV2[11];

        public PlayerEquipementHolderV2[] Equipement => m_Equipement;

        public void SetSaveEquipement(PlayerEquipementHolderV2[] item)
        {
            m_Equipement = item;
        }
    }
}