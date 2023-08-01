using System;
using KarpysDev.Script.Manager.Library;
using KarpysDev.Script.Utils;
using TMPro;
using UnityEngine;

namespace KarpysDev.Script.Entities.BuffRelated
{
    public abstract class Buff : MonoBehaviour
    {
        [SerializeField] protected Transform m_VisualTransform = null;
        [SerializeField] protected BuffType m_BuffType = BuffType.None;
        [SerializeField] protected BuffGroup m_BuffGroup = BuffGroup.Neutral;
        [SerializeField] protected BuffCooldown m_BuffCooldown = BuffCooldown.Cooldown;
        [SerializeField] protected bool m_StackablePassive = false;

        [Header("Buff Info")] 
        [SerializeField]protected BuffInfo m_BuffInfo;
        [Header("Enemy Specific")]
        [SerializeField] protected bool m_EnemyBuffIgnoreFirstCooldown = false;

        protected BoardEntity m_Caster = null;
        protected BoardEntity m_Receiver = null;
        protected int m_Cooldown = 0;
        protected float m_BuffValue = 0;

        private bool m_IgnoreCooldownOnInit = false;
        private string m_BuffKey = String.Empty;

        public BuffCooldown BuffCooldown => m_BuffCooldown;
        public BuffType BuffType => m_BuffType;
        public BuffGroup BuffGroup => m_BuffGroup;
        public float BuffValue => m_BuffValue;
        public int Cooldown => m_Cooldown;
        public string BuffKey => m_BuffKey;
        public BuffInfo BuffInfo => m_BuffInfo;

        public virtual void InitializeAsBuff(BoardEntity caster,BoardEntity receiver, int cooldown, float buffValue, object[] args = null)
        {
            if (receiver.EntityGroup == EntityGroup.Enemy && m_EnemyBuffIgnoreFirstCooldown)
                m_IgnoreCooldownOnInit = true;
        
            m_Receiver = receiver;
            m_Caster = caster;
            m_Cooldown = cooldown;
            m_BuffValue = buffValue;
            m_Receiver.Buffs.AddBuff(this);
            Apply();
        }
        protected abstract void Apply();
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
            Destroy(gameObject);
        }

        public void SetBuffCooldown(BuffCooldown cooldown)
        {
            m_BuffCooldown = cooldown;
        }

        public void SetBuffKey(string buffKey)
        {
            m_BuffKey = buffKey;
        }

        public void EnableVisual(bool enable)
        {
            if(m_VisualTransform)
                m_VisualTransform.gameObject.SetActive(enable); 
        }
        
        protected virtual Buff ContainPassiveCheck()
        {
            return m_Receiver.Buffs.ContainPassiveOfType(m_BuffType);
        }

        protected virtual bool RemovePassiveCheck()
        {
            return m_BuffValue == 0;
        }
        
        public virtual void InitializeAsPassive(BoardEntity caster,BoardEntity receiver,float buffValue,object[] args = null)
        {
            m_Receiver = receiver;
            m_Caster = caster;
            m_BuffValue = buffValue;
            
            //Si le caster a deja un passif de ce type => ajouter sa valeur a celle deja existante, sinon l'ajouter normalement ?
            Buff passive = ContainPassiveCheck(); 
            if (m_StackablePassive && passive)
            {
                passive.AddPassiveValue(buffValue);
                Destroy(gameObject);
            }
            else
            {
                m_Receiver.Buffs.AddPassive(this);
                Apply();
            }
        }

        protected virtual void AddPassiveValue(float value)
        {
            m_BuffValue += value;
            OnPassiveValueChanged();
            m_Receiver.ComputeAllSpells();
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

        protected virtual void OnPassiveValueChanged() 
        {}

        private void RemovePassive()
        {
            m_Receiver.Buffs.RemovePassive(this);
            UnApply();
            Destroy(gameObject);
        }

        public string GetDescription()
        {
            return StringUtils.GetDescription(m_BuffInfo.BaseBuffDescription, GetDescriptionDynamicValues());
        }

        protected virtual string[] GetDescriptionDynamicValues()
        {
            return m_BuffValue.ToString("0").ToSingleArray();
        }

        public virtual object[] GetArgs()
        {
            return null;
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