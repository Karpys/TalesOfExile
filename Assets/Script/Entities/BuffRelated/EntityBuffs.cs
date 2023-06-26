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

        public bool IsCursed()
        {
            foreach (Buff buff in m_Buffs)
            {
                if (buff.BuffGroup == BuffGroup.Curse)
                    return true;
            }

            return false;
        }

        public BuffType[] GetCurseTypes()
        {
            return m_Buffs.Where(b => b.BuffGroup == BuffGroup.Curse).Select(b => b.BuffType).ToArray();
        }

        public List<BuffState> GetCurseStates()
        {
            List<BuffState> curseStates = new List<BuffState>();
            foreach (Buff buff in m_Buffs)
            {
                if(buff.BuffGroup != BuffGroup.Curse)
                    continue;
                curseStates.Add(new BuffState(buff.BuffType,buff.Cooldown,buff.BuffValue,buff.GetArgs()));
            }

            return curseStates;
        }
    }

    [Serializable]
    public enum BuffGroup
    {
        Neutral,
        Buff,
        Debuff,
        Curse
    }

    [Serializable]
    public enum BuffCooldown
    {
        Cooldown,
        Toggle,
        Passive,
    }
    
    public struct BuffState
    {
        public BuffType BuffType;
        public int Duration;
        public float Value;
        public object[] AdditionalDatas;

        public BuffState(BuffType buffType, int duration, float buffValue, object[] additionalDatas)
        {
            BuffType = buffType;
            Duration = duration;
            Value = buffValue;
            AdditionalDatas = additionalDatas;
        }
    }
}