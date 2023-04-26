using UnityEngine;


public class EntityEquipement : MonoBehaviour
{
    private EquipementItem[] m_Equipement = new EquipementItem[11];

    public EquipementItem[] Equipement => m_Equipement;
}