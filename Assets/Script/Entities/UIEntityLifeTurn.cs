using TMPro;
using UnityEngine;

namespace KarpysDev.Script.Entities
{
    public class UIEntityLifeTurn : EntityLifeTurn
    {
        [SerializeField] private TMP_Text m_LifeCount = null;

        protected override void ReduceLifeCounter()
        {
            base.ReduceLifeCounter();
            m_LifeCount.text = m_LifeCount.ToString();
        }
    }
}