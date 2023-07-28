using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace KarpysDev.Script.UI.Pointer
{
    public class ShopBuyUIButtonPointer : UIButtonPointer
    {
        [SerializeField] private Canvas_Shop m_Shop = null;
        [SerializeField] private TMP_Text m_BuyText = null;
        [SerializeField] private Image m_ButtonImage = null;
        [SerializeField] private float m_AlphaOnInactive = 0.5f;

        private bool m_IsActive = true;
        public override void Trigger()
        {
            if(!m_IsActive)
                return;
            m_Shop.Buy();
        }

        public void SetState(bool active)
        {
            if (active)
            {
                m_ButtonImage.color = m_ButtonImage.color.setAlpha(1);
                m_BuyText.color = m_BuyText.color.setAlpha(1);
            }
            else
            {
                m_ButtonImage.color = m_ButtonImage.color.setAlpha(m_AlphaOnInactive);
                m_BuyText.color = m_BuyText.color.setAlpha(m_AlphaOnInactive);
            }

            m_IsActive = active;
        }
    }
}