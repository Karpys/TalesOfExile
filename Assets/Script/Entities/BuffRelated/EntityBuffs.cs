using System;
using System.Collections.Generic;
using System.Linq;
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

    public void TryRemovePassiveOffTypeAndValue(BuffType buffType, float buffValue)
    {
        for (int i = 0; i < m_Buffs.Count; i++)
        {
            Debug.Log(m_Buffs[i].BuffType);
            if (m_Buffs[i].BuffType == buffType && m_Buffs[i].BuffValue == buffValue &&
                m_Buffs[i].BuffCooldown == BuffCooldown.Passive)
            {
                m_Buffs[i].RemovePassive();
                return;
            }
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
    Passive,
}