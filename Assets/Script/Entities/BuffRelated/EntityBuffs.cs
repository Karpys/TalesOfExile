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
            buff.Apply();
        }
        
        public Buff AddToggle(Buff buff)
        {
            m_Buffs.Add(buff);
            OnAddBuff?.Invoke(buff);
            buff.SetBuffCooldown(BuffCooldown.Toggle);
            buff.Apply();
            return buff;
        }

        public void RemoveBuff(Buff buff)
        {
            if (m_Buffs.Contains(buff))
                m_Buffs.Remove(buff);
        
            OnRemoveBuff?.Invoke(buff);
        }

        public void TryAddPassive(PassiveBuffType passiveBuffType, float value,out bool added)
        {
            Buff passive = ContainPassiveOfType(passiveBuffType);
            
            if (passive != null)
            {
                passive.AddPassiveValue(value);
                added = true;
                return;
            }

            added = false;
        }
        public void AddPassive(Buff buff,PassiveBuffType passiveBuffType)
        {
            buff.SetPassiveBuffType(passiveBuffType);
            m_Passive.Add(buff);
            buff.Apply();
        }

        public void RemovePassive(Buff buff)
        {
            if (m_Passive.Contains(buff))
                m_Passive.Remove(buff);
        }
        
        private void Start()
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
        
        public void TryRemovePassive(PassiveBuffType passiveBuffType,float buffValue)
        {
            for (int i = 0; i < m_Passive.Count; i++)
            {
                if (m_Passive[i].PassiveBuffType == passiveBuffType)
                {
                    m_Passive[i].ReducePassiveValue(buffValue);
                    return;
                }
            }
        }

        public Buff ContainPassiveOfType(PassiveBuffType buffType)
        {
            return m_Passive.FirstOrDefault(b => b.PassiveBuffType == buffType);
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
        
        public List<BuffState> GetCurseStates()
        {
            //Todo: Replace with ListOFBuff curses//
            List<BuffState> curseStates = new List<BuffState>();
            foreach (Buff buff in m_Buffs)
            {
                if(buff.BuffGroup != BuffGroup.Curse)
                    continue;
                curseStates.Add(new BuffState(buff.Cooldown,buff.BuffValue));
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
        public int Duration;
        public float Value;

        public BuffState(int duration, float buffValue)
        {
            Duration = duration;
            Value = buffValue;
        }
    }
}