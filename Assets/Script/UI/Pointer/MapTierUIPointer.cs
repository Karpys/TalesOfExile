using System;
using KarpysDev.Script.Manager.Library;
using TMPro;
using UnityEngine;

namespace KarpysDev.Script.UI.Pointer
{
    public class MapTierUIPointer : UIButtonPointer
    {
        [SerializeField] private TMP_Text m_TierText = null;
        private Tier m_Tier = Tier.Tier0;

        public void SetTier(Tier tier)
        {
            m_Tier = tier;
            m_TierText.text = "T" + (int) m_Tier;
        }

        public override void Trigger()
        {
            Canvas_MissionSelection.Instance.DisplayCurrentTier(m_Tier);
        }
    }
}