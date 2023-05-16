using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EntityBuffs : MonoBehaviour
{
    private List<Buff> m_Buffs = new List<Buff>();

    public Action<Buff> OnAddBuff = null;
    public Action<Buff> OnRemoveBuff = null;
    public Action OnCdReduced = null;

    public List<Buff> Buffs => m_Buffs;

    public void AddBuff(Buff buff)
    {
        m_Buffs.Add(buff);
        OnAddBuff?.Invoke(buff);
    }

    public void RemoveBuff(Buff buff)
    {
        if (m_Buffs.Contains(buff))
            m_Buffs.Remove(buff);
        
        OnRemoveBuff?.Invoke(buff);
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
        
        OnCdReduced?.Invoke();
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
    
    public void TryRemoveBuffViaKey(string buffKey)
    {
        for (int i = 0; i < m_Buffs.Count; i++)
        {
            if (m_Buffs[i].BuffKey == buffKey)
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