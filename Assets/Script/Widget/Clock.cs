using System;
using UnityEngine;

public class Clock
{
    private float m_clockDuration = 0;
    private Action m_OnClockEnd = null;
    private bool m_HasTrigger = false;

    public Clock(float duration, Action onClockEnd)
    {
        m_clockDuration = duration;
        m_OnClockEnd = onClockEnd;
    }

    public void UpdateClock()
    {
        if(m_HasTrigger)
            return;
        
        m_clockDuration -= Time.deltaTime;

        if (m_clockDuration <= 0)
        {
            m_HasTrigger = true;   
            m_OnClockEnd?.Invoke();
        }
    }
}
