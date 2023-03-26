using System;
using System.Collections.Generic;
using UnityEngine;

public class EntityBuffs : MonoBehaviour
{
    public List<Buff> m_Buffs = null;

    public void AddBuff(Buff buff)
    {
        m_Buffs.Add(buff);
    }

    public void RemoveBuff(Buff buff)
    {
        if (m_Buffs.Contains(buff))
            m_Buffs.Remove(buff);
    }
    private  void Start()
    {
        GameManager.Instance.A_OnEndTurn += ReduceAllCd;
    }

    private void OnDestroy()
    {
        if(GameManager.Instance)
            GameManager.Instance.A_OnEndTurn -= ReduceAllCd;
    }

    private void ReduceAllCd()
    {
        for (int i = m_Buffs.Count - 1; i >= 0; i--)
        {
            m_Buffs[i].ReduceCooldown();
        }
    }
}

[System.Serializable]
public enum BuffGroup
{
    Neutral,
    Buff,
    Debuff
}

[System.Serializable]
public enum BuffCooldown
{
    Cooldown,
    Toggle,
}