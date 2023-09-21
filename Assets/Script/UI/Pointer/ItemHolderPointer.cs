using KarpysDev.Script.Items;
using KarpysDev.Script.Manager;
using KarpysDev.Script.UI.ItemContainer;
using KarpysDev.Script.UI.ItemContainer.V2;
using KarpysDev.Script.Widget;
using UnityEngine;

namespace KarpysDev.Script.UI.Pointer
{
    public class ItemHolderPointer : UIPointer
    {
        [SerializeField] private ItemUIHolderV2 m_ItemHolder = null;
        [SerializeField] private EquipementItemDescriptionDisplayer m_EquipementDisplayer = null; 
        [SerializeField] private ItemDescriptionDisplayer m_ItemDisplayer = null; 
        [SerializeField] private float m_DisplayDuration = 1f;

        private Clock m_EventClock = null;
        private ItemDescriptionDisplayer m_Displayer = null;

        private bool needReset = false;
        private void Update()
        {
            m_EventClock?.UpdateClock();
        }

        private void LateUpdate()
        {
            if (needReset)
            {
                needReset = false;
                m_ItemHolder.MouseOn = false;
            }
        }

        protected override void OnEnter()
        {
            m_ItemHolder.MouseOn = true;
            ItemUIController.Instance.SetCurrentMouseHolder(m_ItemHolder);
        
            if(m_ItemHolder.AttachedItem == null)
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

            needReset = true;
            //=> m_ItemHolder.MouseOn = false;
        }

        private void DisplayItemDescription()
        {
            if(m_ItemHolder.AttachedItem == null)
                return;
        
            switch (m_ItemHolder.AttachedItem.Data.ObjectType)
            {
                case ObjectType.DefaultObject:
                    m_Displayer = Instantiate(m_ItemDisplayer, transform.position, Quaternion.identity, GlobalCanvas.Instance.transform);
                    m_Displayer.Initialize(m_ItemHolder.AttachedItem);
                    break;
                case ObjectType.Equipement:
                    m_Displayer = Instantiate(m_EquipementDisplayer, transform.position, Quaternion.identity, GlobalCanvas.Instance.transform);
                    m_Displayer.Initialize(m_ItemHolder.AttachedItem);
                    break;
                default:
                    m_Displayer = Instantiate(m_ItemDisplayer, transform.position, Quaternion.identity, GlobalCanvas.Instance.transform);
                    m_Displayer.Initialize(m_ItemHolder.AttachedItem);
                    Debug.LogError("Display type displayer not implemented" + m_ItemHolder.AttachedItem.Data.ObjectType);
                    break;
            }
        }
    }
}