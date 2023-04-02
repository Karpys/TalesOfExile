using UnityEngine;

public class InventoryObjectHolder : MonoBehaviour
{
    [SerializeField] private SpriteRenderer m_InWorldVisual = null;
    [SerializeField] private Transform m_JumpHolder = null;
    
    private InventoryObject m_InventoryObject = null;
    public Transform JumpHolder => m_JumpHolder;
    public InventoryObject InventoryObject => m_InventoryObject;

    //Grab Item Player Inventory//
    public void InitalizeHolder(InventoryObject inventoryObject)
    {
        m_InventoryObject = inventoryObject;
    }
    public void DisplayWorldVisual()
    {
        m_InWorldVisual.sprite = m_InventoryObject.Data.InWorldVisual;
        m_InWorldVisual.gameObject.SetActive(true);
    }
}