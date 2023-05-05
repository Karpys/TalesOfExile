using TMPro;
using UnityEngine;

public class UIEntityLifeTurn : EntityLifeTurn
{
    [SerializeField] private TMP_Text m_LifeCount = null;

    protected override void ReduceLifeCounter()
    {
        base.ReduceLifeCounter();
        m_LifeCount.text = m_LifeCount.ToString();
    }
}