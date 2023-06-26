using System;
using KarpysDev.Script.Entities.EquipementRelated;
using KarpysDev.Script.Utils;
using UnityEngine;

namespace KarpysDev.Script.Entities
{
    public class SummonTransmitter : MonoBehaviour
    {
        [SerializeField] private StatTransmit[] m_StatTransmit = null;
        [SerializeField] private EventTransmit[] m_EventTransmit = null; 
        [SerializeField] private bool m_Recompute = false;

        private BoardEntity m_AttachedEntity = null;
        private BoardEntity m_CopyEntity = null;
        private Modifier[] m_StatModifier = null;

        private void Awake()
        {
            m_AttachedEntity = GetComponent<BoardEntity>();
        }

        private void OnDestroy()
        {
            m_CopyEntity.EntityEvent.OnSpellRecompute -= ApplyStatsTransmitter;
        }

        public void InitTransmitter(BoardEntity copyEntity)
        {
            if (copyEntity == m_AttachedEntity)
            {
                Debug.LogError("Self Init Transmitter ? Exit");
                return;
            }

            m_CopyEntity = copyEntity;

            ApplyEventTransmitter();
            ApplyStatsTransmitter();
            m_CopyEntity.EntityEvent.OnSpellRecompute += ApplyStatsTransmitter;
        }

        private void ApplyEventTransmitter()
        {
            for (int i = 0; i < m_EventTransmit.Length; i++)
            {
                switch (m_EventTransmit[i])
                {
                    case EventTransmit.OnKill:
                        m_AttachedEntity.EntityEvent.OnKill += m_CopyEntity.EntityEvent.OnKill;
                        break;
                }
            }
        }
        
        private void ApplyStatsTransmitter()
        {
            ClearModifier();

            EntityStats stats = m_CopyEntity.EntityStats;

            m_StatModifier = new Modifier[m_StatTransmit.Length];

            for (int i = 0; i < m_StatTransmit.Length; i++)
            {
                StatTransmit transmit = m_StatTransmit[i];
                Modifier modToApply = new Modifier(ModifierType.None, "");

                if (StatsTransmitterUtils.transmitterAction.TryGetValue(transmit.Type, out Action<EntityStats, Modifier, float> action))
                {
                    action.Invoke(stats, modToApply, transmit.StatConvertion);
                }
                else
                {
                    Debug.LogError("Transmit type has not been set up : " + transmit.Type);
                }

                Debug.Log(" mod type" + modToApply.Type + "value" + modToApply.Value);
                ModifierUtils.ApplyModifier(modToApply, m_AttachedEntity);
                m_StatModifier[i] = modToApply;
            }
        
            m_AttachedEntity.ComputeAllSpells();
        }

        private void ClearModifier()
        {
            if(m_StatModifier == null)
                return;
        
            foreach (Modifier modifier in m_StatModifier)
            {
                ModifierUtils.UnapplyModifier(modifier,m_AttachedEntity);
            }

            m_StatModifier = null;
        }
    }

    [Serializable]
    public struct StatTransmit
    {
        public StatType Type;
        [Range(0,1)]
        public float StatConvertion;
    }
    
    public enum EventTransmit
    {
        OnKill,
        
    }

    public enum StatType
    {
        None = 0,
        //Damage Type 0 => 20
        PhysicalDamage = 1,
        ElementalDamage = 2,
        FireDamage = 3,
        ColdDamage = 4,
        HolyDamage = 5,
        //SubDamageType  21 => 30
        MeleeDamage = 21,
        ProjectileDamage = 22,
        SpellDamage = 23,
        WeaponForce = 24,
        //DamageResistance 31 => 50
    }
}