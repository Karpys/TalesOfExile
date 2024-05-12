using System;
using KarpysDev.KarpysUtils;
using KarpysDev.Script.UI;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace KarpysDev.Script.Widget.DebugMenu
{
    public class DebugMenu : SingletonMonoBehavior<DebugMenu>
    {
        [SerializeField] private Transform m_Container = null;
        [SerializeField] private Button m_DebugButton = null;

        public void Open()
        {
            m_Container.gameObject.SetActive(true);
        }

        public void Close()
        {
            m_Container.gameObject.SetActive(false);
        }

        public void AddDebugButton(Action onClick, string buttonName)
        {
            Button button = Instantiate(m_DebugButton, m_Container);
            button.onClick.AddListener(new UnityAction(onClick));
            button.GetComponentInChildren<TMP_Text>().text = buttonName;
        }
    }
}