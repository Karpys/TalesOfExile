using System;
using KarpysDev.Script.Manager.Library;
using TMPro;
using UnityEngine;

namespace KarpysDev.Script.UI.Pointer
{
    public class MapTierUIPointer : UIPointerController
    {
        [SerializeField] private TMP_Text m_TierText = null;
        private Tier m_Tier = Tier.Tier0;

        public Tier Tier => m_Tier;
        public void SetTier(Tier tier)
        {
            m_Tier = tier;
            m_TierText.text = "T" + (int) m_Tier;
        }
    }
}