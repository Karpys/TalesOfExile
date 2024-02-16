using KarpysDev.Script.UI.ItemContainer.V2;
using UnityEngine;

namespace KarpysDev.Script.Entities.EquipementRelated
{
    public class EntityEquipement : MonoBehaviour
    {
        private PlayerEquipementHolder[] m_Equipement = new PlayerEquipementHolder[11];

        public PlayerEquipementHolder[] Equipement => m_Equipement;

        public void SetSaveEquipement(PlayerEquipementHolder[] item)
        {
            m_Equipement = item;
        }
    }
}