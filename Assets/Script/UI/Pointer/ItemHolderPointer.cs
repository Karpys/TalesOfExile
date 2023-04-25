using UnityEngine;

public class ItemHolderPointer : UIPointer
{
    [SerializeField] private ItemUIHolder m_ItemHolder = null;
    [SerializeField] private EquipementItemDescriptionDisplayer m_EquipementDisplayer = null; 
    [SerializeField] private ItemDescriptionDisplayer m_ItemDisplayer = null; 
    [SerializeField] private float m_DisplayDuration = 1f;

    private Clock m_EventClock = null;
    private ItemDescriptionDisplayer m_Displayer = null;
    private void Update()
    {
        m_EventClock?.UpdateClock();
    }


    protected override void OnEnter()
    {
        if(m_ItemHolder.Item == null)
            return;
        
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
        if(m_ItemHolder.Item == null)
            return;
        
        switch (m_ItemHolder.Item.Data.ObjectType)
        {
            case ObjectType.DefaultObject:
                m_Displayer = Instantiate(m_ItemDisplayer, Input.mousePosition, Quaternion.identity, GlobalCanvas.Instance.transform);
                m_Displayer.Initialize(m_ItemHolder.Item);
                break;
            case ObjectType.Equipement:
                m_Displayer = Instantiate(m_EquipementDisplayer, Input.mousePosition, Quaternion.identity, GlobalCanvas.Instance.transform);
                m_Displayer.Initialize(m_ItemHolder.Item);
                break;
            default:
                m_Displayer = Instantiate(m_ItemDisplayer, Input.mousePosition, Quaternion.identity, GlobalCanvas.Instance.transform);
                m_Displayer.Initialize(m_ItemHolder.Item);
                Debug.LogError("Display type displayer not implemented" + m_ItemHolder.Item.Data.ObjectType);
                break;
        }
    }
}