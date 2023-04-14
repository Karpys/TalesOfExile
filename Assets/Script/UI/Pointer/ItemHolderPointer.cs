using UnityEngine;

public class ItemHolderPointer : UIPointer
{
    [SerializeField] private InventoryUIHolder m_ItemHolder = null;
    [SerializeField] private EquipementItemDescriptionDisplayer m_EquipementDisplayer = null; 
    [SerializeField] private ItemDescriptionDisplayer m_ItemDisplayer = null; 
    [SerializeField] private float m_DisplayDuration = 1f;

    private Clock m_EventClock = null;
    private ItemDescriptionDisplayer m_Displayer = null;
    private void Update()
    {
        if(m_EventClock != null)
            m_EventClock.UpdateClock();
    }


    protected override void OnEnter()
    {
        m_EventClock = new Clock(m_DisplayDuration, DisplayItemDescription);
    }

    protected override void OnExit()
    {
        m_EventClock = null;

        if (m_Displayer)
        {
            Destroy(m_Displayer.gameObject);
            m_Displayer = null;
        }
    }

    private void DisplayItemDescription()
    {
        switch (m_ItemHolder.InventoryObject.Data.ObjectType)
        {
            case ObjectType.DefaultObject:
                m_Displayer = Instantiate(m_ItemDisplayer, Input.mousePosition, Quaternion.identity, transform.parent.transform.parent);
                m_Displayer.Initialize(m_ItemHolder.InventoryObject);
                break;
            case ObjectType.Equipement:
                m_Displayer = Instantiate(m_EquipementDisplayer, Input.mousePosition, Quaternion.identity, transform.parent.transform.parent);
                m_Displayer.Initialize(m_ItemHolder.InventoryObject);
                break;
            default:
                m_Displayer = Instantiate(m_ItemDisplayer, Input.mousePosition, Quaternion.identity, transform.parent.transform.parent);
                m_Displayer.Initialize(m_ItemHolder.InventoryObject);
                Debug.LogError("Display type displayer not implemented" + m_ItemHolder.InventoryObject.Data.ObjectType);
                break;
        }
    }
}