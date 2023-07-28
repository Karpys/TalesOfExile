using KarpysDev.Script.Manager;
using KarpysDev.Script.UI.Pointer;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace KarpysDev.Script.Spell
{
    public class UIBuyableHolder : UIPointerController
    {
        [SerializeField] private Image m_IconHolder = null;
        [SerializeField] private TMP_Text m_Text = null;
        [SerializeField] private Transform m_OnSelectState = null;
        private IUIBuyable m_Buyable = null;
        public IUIBuyable Buyable => m_Buyable;

        public void InitializeBuyableHolder(IUIBuyable buyable)
        {
            m_Buyable = buyable;
            m_IconHolder.sprite = buyable.GetIcon();
            m_Text.text = m_Buyable.Price + GoldManager.GOLD_ICON;
        }

        public void OnSelect()
        {
            m_OnSelectState.gameObject.SetActive(true);
        }

        public void Clear()
        {
            m_OnSelectState.gameObject.SetActive(false);
        }
    }
}