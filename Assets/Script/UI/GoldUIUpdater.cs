using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace KarpysDev.Script.UI
{
    public class GoldUIUpdater : MonoBehaviour
    {
        [SerializeField] private List<TMP_Text> m_GoldCountDisplaer = null;

        public void UpdateGoldDisplayer(string newGoldValue)
        {
            foreach (TMP_Text text in m_GoldCountDisplaer)
            {
                text.text = newGoldValue;
            }
        }

        public void AddGoldDisplayer(TMP_Text text)
        {
            m_GoldCountDisplaer.Add(text);
        }

        public void RemoveGoldDisplayer(TMP_Text text)
        {
            m_GoldCountDisplaer.Remove(text);
        }
    }
}