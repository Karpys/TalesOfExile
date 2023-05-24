using System;
using KarpysDev.Script.Spell;
using KarpysDev.Script.Spell.DamageSpell;
using UnityEngine;

namespace KarpysDev.Script.Entities
{
    public class BoardEntityEventHandler : MonoBehaviour
    {
        public Action<IntSocket> OnRequestBlockSpell = null;
        public Action OnDeath = null;
        //TriggerSpellData can be null
        public Action<BoardEntity,DamageSpellTrigger> OnGetDamageFromSpell = null;
        public Action<TriggerSpellData> OnGetHit = null;
        public Action<BaseSpellTrigger> OnRequestCastEvent = null;

        public Action<DamageSpellTrigger,FloatSocket> OnRequestSpellDamage = null;
        public Action OnSpellRecompute = null;
        public Action OnBehave = null;
        public void TriggerGetDamageAction(BoardEntity damageFrom, DamageSpellTrigger spellDamageTrigger)
        {
            OnGetDamageFromSpell?.Invoke(damageFrom,spellDamageTrigger);
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