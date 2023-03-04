using UnityEngine;


public class EntityEquipement : MonoBehaviour
{
    [SerializeField] private EquipementSocket[] m_Equipements = new EquipementSocket[11];
    
    private BoardEntity m_Entity = null;
    public EquipementSocket[] EquipementSockets => m_Equipements;

    public void InitEquipement()
    {
        m_Entity = GetComponent<BoardEntity>();
        
        //Exemple
        //EquipementUtils.InitEquip(m_TestEquipement,m_Entity);
    }
}