using KarpysDev.Script.UI.ItemContainer;
using UnityEngine;

namespace KarpysDev.Script.UI
{
    using KarpysUtils.TweenCustom;

    public class Canvas_Inventory : MonoBehaviour
    {
        [SerializeField] private Transform m_InventoryContainer = null;
        [SerializeField] private RectTransform m_PlayerEquipement = null;
        [SerializeField] private ItemButtonOptionController m_ButtonOptionController = null;
        [SerializeField] private PlayerInventoryUI m_PlayerInventoryUI = null;
    
        private bool m_IsShown = false;
    
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.I))
            {
                OpenInventory();
            }
        }

        public void OpenInventory()
        {
            if (m_IsShown)
            {
                Hide();
            }
            else
            {
                Show();
            }

            m_IsShown = !m_IsShown;
        }
    
        public void Show()
        {
            //Inventory
            m_InventoryContainer.transform.DoKill();
            m_InventoryContainer.gameObject.SetActive(true);
            m_InventoryContainer.transform.localScale = Vector3.zero;
            //m_PlayerInventoryUI.RefreshInventoryDisplay();
            m_InventoryContainer.transform.DoScale(new Vector3(1, 1, 1), 0.3f).SetEase(Ease.EASE_OUT_SIN).OnComplete(() =>
            {
                //Equipement//
                m_PlayerEquipement.transform.DoKill();
                m_PlayerEquipement.transform.DoUIPosition(new Vector3(-100, -100), 0.3f).SetEase(Ease.EASE_OUT_SIN);
                m_PlayerEquipement.gameObject.SetActive(true);
            });
        
        }

        private void Hide()
        {
            //Equipement//
            m_PlayerEquipement.DoUIPosition(new Vector3(-100, -710), 0.3f).SetEase(Ease.EASE_OUT_SIN).OnComplete(() =>
            {
                //Inventory//
                m_InventoryContainer.transform.DoKill();
                m_InventoryContainer.transform.DoScale(Vector3.zero, 0.3f).SetEase(Ease.EASE_OUT_SIN).OnComplete(() =>
                {
                    m_InventoryContainer.gameObject.SetActive(false);
                });
            
            });
        
            m_ButtonOptionController.Clear();
        }
    }
}