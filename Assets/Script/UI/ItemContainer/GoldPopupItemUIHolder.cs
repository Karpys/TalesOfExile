using TMPro;
using UnityEngine;

namespace KarpysDev.Script.UI.ItemContainer
{
    public class GoldPopupItemUIHolder : ItemUIHolder
    {
        [SerializeField] private TMP_Text m_GoldText = null;

        private static string GOLD_ICON = "<sprite name=\"GoldIcon\">";
        
        private float m_GoldValue = 0;
        
        public void UpdateGold()
        {
            m_GoldValue = GetGoldValue();
            m_GoldText.text = m_GoldValue.ToString("0") + "  " + GOLD_ICON;
        }

        private float GetGoldValue()
        {
            if (Item != null)
            {
                return 250f;
            }
            else
            {
                return 0f;
            }
        }
    }
}