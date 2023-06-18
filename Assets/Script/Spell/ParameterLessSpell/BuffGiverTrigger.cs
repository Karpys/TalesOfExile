﻿using KarpysDev.Script.Entities;
using KarpysDev.Script.Entities.BuffRelated;
using KarpysDev.Script.Manager.Library;
using UnityEngine;

namespace KarpysDev.Script.Spell.ParameterLessSpell
{
    public class BuffGiverTrigger : SelectionSpellTrigger
    {
        protected object[] m_BuffArgs = null;

        private BuffGroup m_BuffGroup = BuffGroup.Buff;
        private BuffType m_BuffType = BuffType.None;
        private BuffCooldown m_BuffCooldown = BuffCooldown.Cooldown;
        protected int m_BuffDuration = 0;
        protected float m_BuffValue = 0;

        private Buff m_CurrentToggleBuff = null;

        public Buff CurrentToggleBuff => m_CurrentToggleBuff;
        public BuffGiverTrigger(BaseSpellTriggerScriptable baseScriptable,BuffGroup buffGroup,BuffType buffType,BuffCooldown buffCooldown,int buffDuration,float buffValue) : base(baseScriptable)
        {
            m_BuffGroup = buffGroup;
            m_BuffCooldown = buffCooldown;
            m_BuffType = buffType;
            m_BuffDuration = buffDuration;
            m_BuffValue = buffValue;
        }

        public override void Trigger(TriggerSpellData spellData, SpellTiles spellTiles, CastInfo castInfo)
        {
            if (m_BuffCooldown != BuffCooldown.Toggle)
            {
                base.Trigger(spellData, spellTiles, castInfo);
                return;
            }

            if (m_CurrentToggleBuff != null)
            {
                m_CurrentToggleBuff.RemoveBuff();
                m_CurrentToggleBuff = null;
            }
            else
            {
                base.Trigger(spellData,spellTiles,castInfo);
            }
        }

        protected override void EntityHit(BoardEntity entity, TriggerSpellData spellData,
            Vector2Int spellOrigin, CastInfo castInfo)
        {
            base.EntityHit(entity, spellData, spellOrigin,castInfo);
            GiveBuff(spellData.AttachedEntity,entity,m_BuffArgs);
        }

        private void GiveBuff(BoardEntity caster,BoardEntity receiver,object[] args = null)
        {
            if (m_BuffCooldown == BuffCooldown.Toggle)
            {
                m_CurrentToggleBuff = BuffLibrary.Instance.AddBuffToViaKey(m_BuffType, receiver);
                m_CurrentToggleBuff.SetBuffCooldown(BuffCooldown.Toggle);
                m_CurrentToggleBuff.InitializeAsBuff(caster, receiver, m_BuffDuration, m_BuffValue, args);
            }
            else
            {
                BuffLibrary.Instance.AddBuffToViaKey(m_BuffType, receiver).InitializeAsBuff(caster, receiver, m_BuffDuration, m_BuffValue, args);
            }
        }

        protected override EntityGroup GetEntityGroup(TriggerSpellData spellData)
        {
            if (m_BuffGroup == BuffGroup.Buff)
                return spellData.AttachedEntity.EntityGroup;

            return base.GetEntityGroup(spellData);
        }

        public override string[] GetDescriptionParts()
        {
            string[] descriptionParts = new string[2];
            descriptionParts[0] = Mathf.Floor(m_BuffValue) + "";
            descriptionParts[1] = m_BuffDuration  + "";
            return descriptionParts;
        }

        protected override int GetSpellPriority()
        {
            if(m_BuffCooldown != BuffCooldown.Toggle)
                return base.GetSpellPriority();

            if (m_CurrentToggleBuff == null)
            {
                return m_SpellPriority;
            }
            else
            {
                return 0;
            }
        }
    }
}