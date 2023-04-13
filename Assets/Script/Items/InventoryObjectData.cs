using UnityEngine;

[CreateAssetMenu(fileName = "InventoryData", menuName = "Inventory/InventoryData", order = 0)]
public class InventoryObjectData : ScriptableObject
{
    [Header("UNIQUE TO EACH BASE ITEM")]
    [SerializeField] private int m_UniqueId = -1;
    [Header("Object Type")]
    [SerializeField] private ObjectType m_ObjectType = ObjectType.DefaultObject;
    [Header("Visual Data")]
    [SerializeField] private Sprite m_InWorldVisual = null;
    [SerializeField] private Sprite m_InUIVisual = null;
    [Header("Naming and Base Description")]
    [SerializeField] private string m_ObjectName = null;
    [SerializeField] private string m_Description = null;
    [Header("Rarity")]
    [SerializeField] private Rarity m_BaseRarity = Rarity.Common;

    public int UniqueId => m_UniqueId;
    public ObjectType ObjectType => m_ObjectType;
    public Sprite InWorldVisual => m_InWorldVisual;
    public Sprite InUIVisual => m_InUIVisual;
    public string ObjectName => m_ObjectName;
    public string Description => m_Description;
}

public enum ObjectType
{
    DefaultObject,
    Equipement,
}

public enum Rarity
{
    Common,
    Uncommon,
    Rare,
    Legendary,
}