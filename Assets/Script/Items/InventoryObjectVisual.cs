using UnityEngine;

[CreateAssetMenu(fileName = "InventoryVisual", menuName = "Inventory/Visual", order = 0)]
public class InventoryObjectVisual : ScriptableObject
{
    [SerializeField] private Sprite m_InWorldVisual = null;
    [SerializeField] private Sprite m_InUIVisual = null;

    public Sprite InWorldVisual => m_InWorldVisual;
    public Sprite InUIVisual => m_InUIVisual;
}
