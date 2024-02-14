﻿using System;
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
        protected bool m_StackablePassive = false;

        protected BuffInfo m_BuffInfo;
        protected bool m_EnemyBuffIgnoreFirstCooldown = false;

        protected BoardEntity m_Caster = null;
        protected BoardEntity m_Receiver = null;
        protected int m_Cooldown = 0;
        protected float m_BuffValue = 0;

        private bool m_IgnoreCooldownOnInit = false;

        public BuffCooldown BuffCooldown => m_BuffCooldown;
        public BuffGroup BuffGroup => m_BuffGroup;
        public float BuffValue => m_BuffValue;
        public int Cooldown => m_Cooldown;
        public BuffInfo BuffInfo => m_BuffInfo;
        public PassiveBuffType PassiveBuffType => m_PassiveBuffType;

        public Buff(BoardEntity caster,BoardEntity receiver,BuffType buffType, int cooldown, float buffValue)
        {
            if (receiver.EntityGroup == EntityGroup.Enemy && m_EnemyBuffIgnoreFirstCooldown)
                m_IgnoreCooldownOnInit = true;

            m_BuffType = buffType;
            m_Receiver = receiver;
            m_Caster = caster;
            m_Cooldown = cooldown;
            m_BuffValue = buffValue;
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

        public string GetDescription()
        {
            return StringUtils.GetDescription(m_BuffInfo.BaseBuffDescription, GetDescriptionDynamicValues());
        }

        protected virtual string[] GetDescriptionDynamicValues()
        {
            return m_BuffValue.ToString("0").ToSingleArray();
        }
    }

    [Serializable]
    public struct BuffInfo
    {
        public Sprite BuffVisual;
        public string BuffName;
        [Header("Spell Description (&0..&1) => place holder")]
        public string BaseBuffDescription;
    }
}