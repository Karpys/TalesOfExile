using KarpysDev.Script.Entities;
using KarpysDev.Script.Entities.BuffRelated;
using KarpysDev.Script.Manager.Library;
using KarpysDev.Script.Widget;
using UnityEngine;

namespace KarpysDev.Script.Spell.ParameterLessSpell
{
    public class BuffGiverTrigger : SelectionSpellTrigger
    {
        private BuffType m_BuffType = BuffType.None;
        private BuffCooldown m_BuffCooldown = BuffCooldown.Cooldown;
        protected BuffGroup m_BuffGroup = BuffGroup.Buff;
        protected int m_BuffDuration = 0;
        protected float m_BuffValue = 0;
        private VisualEffectType m_VisualEffectType = VisualEffectType.None;

        private Buff m_CurrentToggleBuff = null;

        public Buff CurrentToggleBuff => m_CurrentToggleBuff;
        public BuffGiverTrigger(BaseSpellTriggerScriptable baseScriptable,BuffGroup buffGroup,BuffType buffType,BuffCooldown buffCooldown,int buffDuration,float buffValue,VisualEffectType visualEffectType) : base(baseScriptable)
        {
            m_BuffGroup = buffGroup;
            m_BuffCooldown = buffCooldown;
            m_BuffType = buffType;
            m_BuffDuration = buffDuration;
            m_BuffValue = buffValue;
            m_VisualEffectType = visualEffectType;
        }

        protected override void Trigger(TriggerSpellData spellData, SpellTiles spellTiles, CastInfo castInfo, float efficiency = 1)
        {
            if (m_BuffCooldown != BuffCooldown.Toggle)
            {
                base.Trigger(spellData, spellTiles, castInfo,efficiency);
                return;
            }

            if (m_CurrentToggleBuff != null)
            {
                m_CurrentToggleBuff.RemoveBuff();
                m_CurrentToggleBuff = null;
            }
            else
            {
                base.Trigger(spellData,spellTiles,castInfo,efficiency);
            }
        }

        protected override void EntityHit(BoardEntity entity, TriggerSpellData spellData,
            Vector2Int spellOrigin, CastInfo castInfo)
        {
            base.EntityHit(entity, spellData, spellOrigin,castInfo);
            GiveBuff(spellData.AttachedEntity,entity);
        }

        private void GiveBuff(BoardEntity caster,BoardEntity receiver)
        {
            Buff buffToAdd = BuffToAdd(caster, receiver);
            if (m_BuffCooldown == BuffCooldown.Toggle)
            {
                m_CurrentToggleBuff = receiver.Buffs.AddToggle(buffToAdd);
            }
            else
            {
                receiver.Buffs.AddBuff(buffToAdd,m_VisualEffectType);
            }
        }

        protected virtual Buff BuffToAdd(BoardEntity caster,BoardEntity receiver)
        {
            return BuffLibrary.Instance.GetBuffViaBuffType(m_BuffType, caster, receiver,m_BuffGroup, m_BuffDuration, m_BuffValue);
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