using System;
using UnityEngine;

public class InventoryObjectHolder : MonoBehaviour
{
    [SerializeField] private SpriteRenderer m_InWorldVisual = null;
    [SerializeField] private Transform m_JumpHolder = null;

    private InventoryObject m_InventoryObject = null;
    public InventoryObject InventoryObject => m_InventoryObject;
    
    //Pick up variables
    private PlayerBoardEntity m_PlayerControllerEntity = null;
    private Vector2Int m_HolderMapPosition = Vector2Int.zero;
    public Transform JumpHolder => m_JumpHolder;

    private void Start()
    {
        m_PlayerControllerEntity = GameManager.Instance.PlayerEntity;
        GameManager.Instance.A_OnEndTurn += CheckForPickUp;
    }

    private void OnDestroy()
    {
        if(GameManager.Instance)
            GameManager.Instance.A_OnEndTurn -= CheckForPickUp;
    }

    //Grab Item Player Inventory//
    private void CheckForPickUp()
    {
        if (m_PlayerControllerEntity.EntityPosition == m_HolderMapPosition)
        {
            m_PlayerControllerEntity.PlayerInventory.PickUp(m_InventoryObject);
            Destroy(gameObject);
        }
    }
    public void InitalizeHolder(InventoryObject inventoryObject,Vector2Int mapPosition)
    {
        m_InventoryObject = inventoryObject;
        m_HolderMapPosition = mapPosition;
    }
    public void DisplayWorldVisual()
    {
        m_InWorldVisual.sprite = m_InventoryObject.Data.InWorldVisual;
        m_InWorldVisual.gameObject.SetActive(true);
    }
}