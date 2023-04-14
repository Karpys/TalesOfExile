using Script.UI;
using TMPro;
using UnityEngine;

public class EquipementItemDescriptionDisplayer : ItemDescriptionDisplayer
{
    [Header("Equipement Parameters")]
    [SerializeField] private AdaptUILayoutSize m_ModifierLayout = null;
    [SerializeField] private TMP_Text m_ModifierTextPrefab = null; 

    public override void Initialize(InventoryObject inventoryObject)
    {
        Debug.Log("Init");
        EquipementObject equipementObject = (EquipementObject) inventoryObject;
        Modifier[] modifiers = equipementObject.ItemModifiers;

        for (int i = 0; i < modifiers.Length; i++)
        {
            TMP_Text modifierText = Instantiate(m_ModifierTextPrefab, m_ModifierLayout.transform);
            modifierText.text = modifiers[i].Type.ToString() + " " + modifiers[i].Value;
        }
        
        m_ModifierLayout.AdaptSize();
        base.Initialize(inventoryObject);
    }
}