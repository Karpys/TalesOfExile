using System;
using System.Collections.Generic;
using KarpysDev.Script.Entities.BuffRelated;
using KarpysDev.Script.Spell;
using KarpysDev.Script.Spell.DamageSpell;
using UnityEngine;

namespace KarpysDev.Script.Entities
{
    public class BoardEntityEventHandler : MonoBehaviour
    {
        public Action OnDeath = null;
        //TriggerSpellData can be null
        public Action<BoardEntity,DamageSpellTrigger> OnGetHitFromSpell = null;
        public Action<BoardEntity,DamageSource,TriggerSpellData> OnGetDamageFromSpell = null;
        public Action<BaseSpellTrigger> OnRequestCastEvent = null;

        public Action<DamageSpellTrigger,FloatSocket> OnRequestBonusSpellDamage = null;
        public Action<DamageSpellTrigger> OnRequestAdditionalSpellSource = null;

        public Action<BoardEntity> OnKill = null;
        public Action OnSpellRecompute = null;
        public Action OnBehave = null;
        
        //Buff
        private Dictionary<BuffCategory, Action<Buff>> m_OnBuffAppliedModifications = new Dictionary<BuffCategory, Action<Buff>>(); 
        private Action<Buff> m_OnBuffApplied = null;

        public void OnBuffApplied(BuffCategory[] categories,Buff buff)
        {
            foreach (BuffCategory buffCategory in categories)
            {
                if(m_OnBuffAppliedModifications.TryGetValue(buffCategory,out var modif))
                {
                    modif?.Invoke(buff);
                }
            }
        }
        public void AddBuffAppliedCategoryModification(BuffCategory targetCategory,Action<Buff> buffModification)
        {
            if (m_OnBuffAppliedModifications.TryGetValue(targetCategory, out Action<Buff> modif))
            {
                modif += buffModification;
            }
            else
            {
                Action<Buff> actionModif = null;
                actionModif += buffModification;
                m_OnBuffAppliedModifications.Add(targetCategory,actionModif);
            }
        }
        public void RemoveBuffAppliedCategoryModification(BuffCategory targetCategory,Action<Buff> buffModification)
        {
            if (m_OnBuffAppliedModifications.TryGetValue(targetCategory, out Action<Buff> modif))
            {
                modif -= buffModification;

                if (modif.GetInvocationList().Length == 0)
                    m_OnBuffAppliedModifications.Remove(targetCategory);
            }
        }
        
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

    [System.Serializable]
    public class FloatSocket
    {
        private float m_Value = 0;

        public float Value
        {
            get => m_Value;
            set => m_Value = value;
        }

        public FloatSocket(float baseValue)
        {
            m_Value = baseValue;
        }
    
        public FloatSocket()
        {
        }
    }
}