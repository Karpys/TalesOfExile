using System;
using KarpysDev.Script.Manager.Library;
using KarpysDev.Script.Utils;
using TMPro;
using UnityEngine;

namespace KarpysDev.Script.Entities.BuffRelated
{
    public abstract class Buff
    {
        protected BuffType m_BuffType = BuffType.None;
        protected PassiveBuffType m_PassiveBuffType = PassiveBuffType.None;
        protected BuffGroup m_BuffGroup = BuffGroup.Neutral;
        protected BuffCooldown m_BuffCooldown = BuffCooldown.Cooldown;

        protected bool m_EnemyBuffIgnoreFirstCooldown = false;

        protected BoardEntity m_Caster = null;
        protected BoardEntity m_Receiver = null;
        protected int m_Cooldown = 0;
        protected float m_BuffValue = 0;

        private bool m_IgnoreCooldownOnInit = false;

        public BuffCooldown BuffCooldown => m_BuffCooldown;
        public BuffGroup BuffGroup => m_BuffGroup;
        public float BuffValue
        {
            get => m_BuffValue;
            set => m_BuffValue = value;
        }

        public int Cooldown
        {
            get => m_Cooldown;
            set => m_Cooldown = value;
        }

        public PassiveBuffType PassiveBuffType => m_PassiveBuffType;
        public BuffType BuffType => m_BuffType;
        public BoardEntity Caster => m_Caster;
        public BoardEntity Receiver => m_Receiver;

        public Buff(BoardEntity caster,BoardEntity receiver,BuffType buffType,BuffGroup buffGroup, int cooldown, float buffValue)
        {
            if (receiver.EntityGroup == EntityGroup.Enemy && m_EnemyBuffIgnoreFirstCooldown)
                m_IgnoreCooldownOnInit = true;

            m_BuffType = buffType;
            m_Receiver = receiver;
            m_Caster = caster;
            m_Cooldown = cooldown;
            m_BuffValue = buffValue;
            m_BuffGroup = buffGroup;
        }

        public abstract void Apply();
        protected abstract void UnApply();

        public void ReduceCooldown()
        {
            if (m_IgnoreCooldownOnInit)
            {
                m_IgnoreCooldownOnInit = false;
                return;
            }
        
            if (m_BuffCooldown == BuffCooldown.Cooldown)
            {
                m_Cooldown -= 1;

                if (m_Cooldown <= 0)
                {
                    RemoveBuff();
                }
            }
        }

        public void RemoveBuff()
        {
            m_Receiver.Buffs.RemoveBuff(this);
            UnApply();
        }

        public void SetBuffCooldown(BuffCooldown cooldown)
        {
            m_BuffCooldown = cooldown;
        }

        protected virtual bool RemovePassiveCheck()
        {
            return m_BuffValue == 0;
        }
        
        public virtual void AddPassiveValue(float value)
        {
            m_BuffValue += value;
            OnPassiveValueChanged();
        }
        
        public virtual void ReducePassiveValue(float value)
        {
            m_BuffValue -= value;

            if (RemovePassiveCheck())
            {
                RemovePassive();
            }

            OnPassiveValueChanged();
            m_Receiver.ComputeAllSpells();
        }

        public void SetPassiveBuffType(PassiveBuffType passiveBuffType)
        {
            m_PassiveBuffType = passiveBuffType;
        }
        
        protected virtual void OnPassiveValueChanged() 
        {}

        private void RemovePassive()
        {
            m_Receiver.Buffs.RemovePassive(this);
            UnApply();
        }

        public string GetDescription(string baseBuffDescription)
        {
            return StringUtils.GetDescription(baseBuffDescription, GetDescriptionDynamicValues());
        }

        protected virtual string[] GetDescriptionDynamicValues()
        {
            return m_BuffValue.ToString("0").ToSingleArray();
        }
    }

    [Serializable]
    public class BuffInfo
    {
        public Sprite BuffVisual = null;
        public string BuffName = String.Empty;
        [Header("Spell Description (&0..&1) => place holder")]
        public string BaseBuffDescription = String.Empty;
    }
}