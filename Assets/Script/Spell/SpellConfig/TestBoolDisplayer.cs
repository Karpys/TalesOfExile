namespace KarpysDev.Script.Spell.SpellConfig
{
    using System;
    using TMPro;
    using UnityEngine;
    using UnityEngine.UI;

    public class TestBoolDisplayer : MonoBehaviour
    {
        [SerializeField] private TMP_Text m_TextMessage = null;
        [SerializeField] private Toggle m_Toggle = null;
        
        public void Initialize(Action<bool> onBoolValueChanged,bool currentValue,string message)
        {
            m_Toggle.isOn = currentValue;
            m_TextMessage.text = message;
            m_Toggle.onValueChanged.AddListener((_) => onBoolValueChanged.Invoke(m_Toggle.isOn));
        }
    }
}