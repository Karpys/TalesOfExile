using UnityEngine;

[CreateAssetMenu(menuName = "Object/Equipement", fileName = "NewEquipement", order = 0)]
public class EquipementInventoryObjectScriptable : InventoryObjectScriptable
{
    [SerializeField] private Equipement m_Equipement = null;
    public override InventoryObject ToIventoryObject()
    {
        return new Equipement(m_Equipement);
    }
}