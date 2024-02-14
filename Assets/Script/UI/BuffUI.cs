using System;
using KarpysDev.Script.Entities.BuffRelated;
using KarpysDev.Script.UI.Pointer;
using KarpysDev.Script.Widget;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace KarpysDev.Script.UI
{
    using Manager.Library;

    public class BuffUI : UIPointer
    {
        [SerializeField] private Image m_BuffVisual = null;
        [SerializeField] private TMP_Text m_BuffCooldown = null;
        [SerializeField] private float m_DisplayDelay = 0.2f;
    
        private BuffUIDisplayer m_Displayer = null;
        private bool m_InDisplay = false;
        
        private Buff m_AttachedBuff = null;
        private Clock m_DisplayerClock = null;
        private BuffInfo m_BuffInfo;

        public Buff AttachedBuff => m_AttachedBuff;

        private void Update()
        {
            m_DisplayerClock?.UpdateClock();
        }

        public void Initialize(Buff buff,BuffUIDisplayer displayer)
        {
            m_Displayer = displayer;
            m_AttachedBuff = buff;
            m_BuffInfo = BuffLibrary.Instance.GetBuffInfoViaType(buff.BuffType);
            m_BuffVisual.sprite = m_BuffInfo.BuffVisual;
            UpdateText();
        }

        public void UpdateText()
        {
            m_BuffCooldown.text = m_AttachedBuff.Cooldown + "";
        }

        protected override void OnEnter()
        {
            m_DisplayerClock = new Clock(m_DisplayDelay, DisplayBuffDescription);
        }

        protected override void OnExit()
        {
            m_DisplayerClock = null;
            
            if (m_InDisplay)
            {
                m_Displayer.Hide();
                m_InDisplay = false;
            }
        }

        private void DisplayBuffDescription()
        {
            m_InDisplay = true;
            m_Displayer.Initialize(m_AttachedBuff,m_BuffInfo,transform.position);
        }
    }
}