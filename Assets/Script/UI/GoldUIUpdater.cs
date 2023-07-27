using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace KarpysDev.Script.UI
{
    public class GoldUIUpdater : MonoBehaviour
    {
        [SerializeField] private List<TMP_Text> m_GoldCountDisplaer = null;

        private string m_LastGoldValueKnonw = String.Empty;
        private static string GOLD_ICON = " <sprite name=\"GoldIcon\">";
        public void UpdateGoldDisplayer(string newGoldValue)
        {
            newGoldValue += GOLD_ICON;
            foreach (TMP_Text text in m_GoldCountDisplaer)
            {
                text.text = newGoldValue;
            }

            m_LastGoldValueKnonw = newGoldValue;
        }

        public void AddGoldDisplayer(TMP_Text text)
        {
            m_GoldCountDisplaer.Add(text);
            text.text = m_LastGoldValueKnonw;
        }

        public void RemoveGoldDisplayer(TMP_Text text)
        {
            m_GoldCountDisplaer.Remove(text);
        }
    }
}