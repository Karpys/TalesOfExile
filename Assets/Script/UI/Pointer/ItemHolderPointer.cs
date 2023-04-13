using UnityEngine;

public class ItemHolderPointer : UIPointer
{
    [SerializeField] private InventoryUIHolder m_ItemHolder = null;
    [SerializeField] private EquipementItemDescriptionDisplayer m_DisplayerPrefab = null; 
    [SerializeField] private float m_DisplayDuration = 1f;

    private Clock m_EventClock = null;
    private EquipementItemDescriptionDisplayer m_Displayer = null;
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
        m_Displayer = Instantiate(m_DisplayerPrefab, Input.mousePosition, Quaternion.identity, transform.parent.transform.parent);
        m_Displayer.Initialize(m_ItemHolder.InventoryObject);
    }
}