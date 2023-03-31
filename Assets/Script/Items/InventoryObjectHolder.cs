using UnityEngine;

public class InventoryObjectHolder : MonoBehaviour
{
    [SerializeField] private SpriteRenderer m_InWorldVisual = null;
    
    private InventoryObject m_InventoryObject = null;
    public InventoryObject InventoryObject => m_InventoryObject;

    //Grab Item Player Inventory//
    public void SetInventoryObject(InventoryObject inventoryObject)
    {
        m_InventoryObject = inventoryObject;
        m_InWorldVisual.sprite = m_InventoryObject.VisualData.InWorldVisual;
    }
    public void DisplayWorldVisual()
    {
        m_InWorldVisual.gameObject.SetActive(true);
    }
}