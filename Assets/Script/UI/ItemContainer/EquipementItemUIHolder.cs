using UnityEngine;

public class EquipementItemUIHolder : ItemUIHolder
{
    [Header("Equipement Specific")] 
    [SerializeField] private Sprite m_EquipementShadow = null;
    [SerializeField] private EquipementType m_EquipementType = EquipementType.Null;

    public EquipementType EquipementType => m_EquipementType;
    public EquipementItem EquipementItem => Item as EquipementItem;
    protected override void DefaultDisplay()
    {
        m_ItemVisual.sprite = m_EquipementShadow;
        base.DefaultDisplay();
    }
}