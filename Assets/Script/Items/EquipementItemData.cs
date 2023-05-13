using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EquipementItemData", menuName = "Inventory/EquipementItemData", order = 0)]
public class EquipementItemData : InventoryItemData
{
    [Header("Equipement Data")]
    [SerializeField] private EquipementType m_EquipementType = EquipementType.Null;
    [SerializeField] private Tier m_EquipementTier = Tier.Tier1;
    [SerializeField] private List<Modifier> m_EquipementBaseModifiers = new List<Modifier>();
    [SerializeField] private ModifierPoolScriptable m_ModifierPool = null;

    public EquipementType EquipementType => m_EquipementType;
    public List<Modifier> EquipementBaseModifiers => m_EquipementBaseModifiers;
    public Tier EquipementTier => m_EquipementTier;
    public ModifierPoolScriptable ModifierPool => m_ModifierPool;
}