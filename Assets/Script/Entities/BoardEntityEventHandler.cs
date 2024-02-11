﻿using System;
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