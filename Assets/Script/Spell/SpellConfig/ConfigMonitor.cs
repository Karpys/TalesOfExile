namespace KarpysDev.Script.Spell.SpellConfig
{
    using System;
    using System.Collections.Generic;
    using UI;
    using UI.Pointer;
    using UnityEngine;

    public class ConfigMonitor : MonoBehaviour
    {
        [SerializeField] private AdaptUILayoutSize m_AdaptUISize = null;
        [SerializeField] private Transform m_Container = null;
        [SerializeField] private Transform m_ConfigLayout = null;
        [SerializeField] private BoolConfigDisplayer boolConfigConfig = null;

        private List<GameObject> m_CurrentConfigs = new List<GameObject>();
        public void DisplayBool(Action<bool> onBoolValueChanged,bool currentValue,string message)
        {
            BoolConfigDisplayer boolConfigDisplayer = Instantiate(boolConfigConfig, m_ConfigLayout);
            boolConfigDisplayer.Initialize(onBoolValueChanged,currentValue,message);
            m_CurrentConfigs.Add(boolConfigDisplayer.gameObject);
        }

        public void Display(IConfig config)
        {
            StartDisplay();
            config.DisplayConfig(this);
            EndDisplay();
        }

        public void Hide()
        {
            m_Container.gameObject.SetActive(false);
        }

        private void ClearOld()
        {
            for (int i = m_CurrentConfigs.Count - 1; i >= 0; i--)
            {
                DestroyImmediate(m_CurrentConfigs[i]);
            }
            
            m_CurrentConfigs.Clear();
        }
        private void StartDisplay()
        {
            m_Container.gameObject.SetActive(true);
            ClearOld();
        }
        
        private void EndDisplay()
        {
            m_AdaptUISize.AdaptSize();
        }
    }
}