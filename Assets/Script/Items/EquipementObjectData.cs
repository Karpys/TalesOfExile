using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "InventoryData", menuName = "Inventory/EquipementData", order = 0)]
public class EquipementObjectData : InventoryObjectData
{
    [Header("Equipement Data")]
    [SerializeField] private EquipementType m_EquipementType = EquipementType.Null;
    [SerializeField] private List<Modifier> m_EquipementBaseModifiers = new List<Modifier>();

    public EquipementType EquipementType => m_EquipementType;
    public List<Modifier> EquipementBaseModifiers => m_EquipementBaseModifiers;
}