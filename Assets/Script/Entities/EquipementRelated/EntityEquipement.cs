using UnityEngine;


public class EntityEquipement : MonoBehaviour
{
    [SerializeField] private EquipementSocket[] m_Equipements = new EquipementSocket[11];
                            
    private BoardEntity m_Entity = null;
    public EquipementSocket[] EquipementSockets => m_Equipements;
}