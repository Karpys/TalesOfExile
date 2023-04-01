using UnityEngine;

public class InventoryObjectHolder : MonoBehaviour
{
    [SerializeField] private SpriteRenderer m_InWorldVisual = null;
    
    private InventoryObject m_InventoryObject = null;
    public InventoryObject InventoryObject => m_InventoryObject;

    //Grab Item Player Inventory//
    public void InitalizeHolder(InventoryObject inventoryObject)
    {
        m_InventoryObject = inventoryObject;

        EquipementObject equipementObject = inventoryObject as EquipementObject;
        foreach (Modifier modifier in equipementObject.Modifiers)
        {
            Debug.Log("Modifier value: " + modifier.Value);
        }
    }
    public void DisplayWorldVisual()
    {
        m_InWorldVisual.sprite = m_InventoryObject.Data.InWorldVisual;
        m_InWorldVisual.gameObject.SetActive(true);
    }
}