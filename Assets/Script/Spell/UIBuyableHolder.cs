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
        private IUIBuyable m_Buyable = null;

        private static string GOLD_ICON = " <sprite name=\"GoldIcon\">";
        public IUIBuyable Buyable => m_Buyable;

        public void InitializeBuyableHolder(IUIBuyable buyable)
        {
            m_Buyable = buyable;
            m_IconHolder.sprite = buyable.GetIcon();
            m_Text.text = m_Buyable.Price + GOLD_ICON;
        }
    }
}