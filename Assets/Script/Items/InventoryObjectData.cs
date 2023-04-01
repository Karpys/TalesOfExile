using UnityEngine;

[CreateAssetMenu(fileName = "InventoryData", menuName = "Inventory/InventoryData", order = 0)]
public class InventoryObjectData : ScriptableObject
{
    [Header("Visual Data")]
    [SerializeField] private Sprite m_InWorldVisual = null;
    [SerializeField] private Sprite m_InUIVisual = null;
    [Header("Naming and Base Description")]
    [SerializeField] private string m_Name = null;
    [SerializeField] private string m_Description = null;

    
    public Sprite InWorldVisual => m_InWorldVisual;
    public Sprite InUIVisual => m_InUIVisual;
    public string Name => m_Name;
    public string Description => m_Description;
}
