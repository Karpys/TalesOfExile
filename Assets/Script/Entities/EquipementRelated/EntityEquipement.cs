using UnityEngine;


public class EntityEquipement : MonoBehaviour
{
    [SerializeField] private EquipementSocket[] m_Equipements = new EquipementSocket[11];
                            
    private BoardEntity m_Entity = null;
    public EquipementSocket[] EquipementSockets => m_Equipements;

    public void InitEquipement(EquipementInventoryObjectScriptable[] startEquipement)
    {
        m_Entity = GetComponent<BoardEntity>();
        
        foreach (EquipementInventoryObjectScriptable equipement in startEquipement)
        {
            EquipementUtils.InitEquip(equipement.ToIventoryObject() as Equipement, m_Entity);
        }
    }
}