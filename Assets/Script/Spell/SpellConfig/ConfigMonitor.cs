namespace KarpysDev.Script.Spell.SpellConfig
{
    using System;
    using System.Collections.Generic;
    using UI;
    using UI.Pointer;
    using UnityEngine;

    public class ConfigMonitor : UIPointer
    {
        [SerializeField] private AdaptUILayoutSize m_AdaptUISize = null;
        [SerializeField] private Transform m_Container = null;
        [SerializeField] private Transform m_ConfigLayout = null;
        [SerializeField] private TestBoolDisplayer m_BoolConfig = null;

        private List<GameObject> m_CurrentConfigs = new List<GameObject>();
        public void DisplayBool(Action<bool> onBoolValueChanged,bool currentValue,string message)
        {
            TestBoolDisplayer testBoolDisplayer = Instantiate(m_BoolConfig, m_ConfigLayout);
            testBoolDisplayer.Initialize(onBoolValueChanged,currentValue,message);
            m_CurrentConfigs.Add(testBoolDisplayer.gameObject);
        }

        public void Display(IConfig config,Vector3 position)
        {
            transform.position = position;
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

        protected override void OnEnter()
        {
            return;
        }

        protected override void OnExit()
        {
            Hide();
        }
    }
}