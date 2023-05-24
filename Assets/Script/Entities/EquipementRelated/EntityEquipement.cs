using KarpysDev.Script.Items;
using UnityEngine;

namespace KarpysDev.Script.Entities.EquipementRelated
{
    public class EntityEquipement : MonoBehaviour
    {
        private Item[] m_Equipement = new Item[11];

        public Item[] Equipement => m_Equipement;

        public void SetSaveEquipement(Item[] item)
        {
            m_Equipement = item;
        }
    }
}