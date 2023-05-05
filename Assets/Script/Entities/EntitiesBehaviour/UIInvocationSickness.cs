using System;
using TMPro;
using UnityEngine;

public class UIInvocationSickness : InvocationSickness
{
    [SerializeField] private TMP_Text m_Count = null;

    private void Start()
    {
        m_Count.text = m_TurnWaitBehave.ToString();
    }

    protected override void EntityCanBehave()
    {
        base.EntityCanBehave();
        m_Count.text = m_TurnWaitBehave.ToString();
    }

    protected override void RemoveSickness()
    {
        Destroy(m_Count.gameObject);
        base.RemoveSickness();
    }
}