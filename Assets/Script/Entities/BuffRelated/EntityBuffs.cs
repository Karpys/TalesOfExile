using System;
using System.Collections.Generic;
using System.Linq;
using KarpysDev.Script.Manager;
using KarpysDev.Script.Manager.Library;
using UnityEngine;

namespace KarpysDev.Script.Entities.BuffRelated
{
    public class EntityBuffs : MonoBehaviour
    {
        private List<Buff> m_Buffs = new List<Buff>();
        private List<Buff> m_Passive = new List<Buff>();

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
        
        public void AddPassive(Buff buff)
        {
            m_Passive.Add(buff);
        }

        public void RemovePassive(Buff buff)
        {
            if (m_Passive.Contains(buff))
                m_Passive.Remove(buff);
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
        
        public void TryRemovePassive(BuffType buffType,float buffValue)
        {
            for (int i = 0; i < m_Passive.Count; i++)
            {
                if (m_Passive[i].BuffType == buffType)
                {
                    m_Passive[i].ReducePassiveValue(buffValue);
                    return;
                }
            }
        }

        public Buff ContainPassiveOfType(BuffType buffType)
        {
            return m_Passive.FirstOrDefault(b => b.BuffType == buffType);
        }
    }

    [Serializable]
    public enum BuffGroup
    {
        Neutral,
        Buff,
        Debuff
    }

    [Serializable]
    public enum BuffCooldown
    {
        Cooldown,
        Toggle,
        Passive,
    }
}