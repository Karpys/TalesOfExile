using System;
using UnityEngine;

public class BoardEntityEventHandler : MonoBehaviour
{
    public Action<IntSocket> OnRequestBlockSpell = null;
    public Action<BoardEntity,BoardEntity> OnPhysicalDamageDone = null;
}

public class IntSocket
{
    private int m_Value = 0;

    public int Value
    {
        get => m_Value;
        set => m_Value = value;
    }

    public IntSocket(int baseValue)
    {
        m_Value = baseValue;
    }
}
