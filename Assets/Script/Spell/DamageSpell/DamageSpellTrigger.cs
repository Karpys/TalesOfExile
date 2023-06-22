using System;
using System.Collections.Generic;
using KarpysDev.Script.Entities;
using KarpysDev.Script.Manager;
using KarpysDev.Script.Widget;
using UnityEngine;

//DAMAGE SPELL TRIGGER need to inherit from BaseTargetEntitySpell abstract class//
//return a List of Entity and then here apply all the damage source//
//The animation part can be move to BaseTargetEntitySpell//
namespace KarpysDev.Script.Spell.DamageSpell
{
    public class DamageSpellTrigger : SelectionSpellTrigger
    {
        protected DamageParameters m_DamageSpellParams = null;

        protected Dictionary<SubDamageType, DamageSource> m_DamageSources = new Dictionary<SubDamageType, DamageSource>();

        public Dictionary<SubDamageType, DamageSource> DamageSources => m_DamageSources;

        private bool m_DisplayDamage = false;
        public DamageSpellTrigger(DamageSpellScriptable damageSpellData):base(damageSpellData)
        {
            Debug.Log("Call Base Damage Spell Trigger");
            m_DamageSpellParams = new DamageParameters(damageSpellData.BaseDamageParameters);
        }

        public override void SetAttachedSpell(SpellData spellData, int priority)
        {
            base.SetAttachedSpell(spellData, priority);
            m_DisplayDamage = m_AttachedSpell.AttachedEntity.EntityGroup != EntityGroup.Enemy;
        }

        #region ComputePart

    

        public override void ComputeSpellData(BoardEntity entity)
        {
            m_DamageSources.Clear();

            FloatSocket bonusDamage = new FloatSocket();
            entity.EntityEvent.OnRequestBonusSpellDamage?.Invoke(this,bonusDamage);
        
            ComputeSpellDamage(entity);
            ApplyDamageModifier(entity,bonusDamage.Value);
        
            //DESIGN CHOICE : Additional damage are added after the damage modifier are applied
            //additional damage scale their own way => ex FireHandBuff
            entity.EntityEvent.OnRequestAdditionalSpellSource?.Invoke(this);
            base.ComputeSpellData(entity);
        }

        private void ApplyDamageModifier(BoardEntity entity,float bonusModifier)
        {
            float damageModifier = DamageManager.GetDamageModifier(m_DamageSpellParams, entity.EntityStats, bonusModifier);
        
            foreach (DamageSource source in m_DamageSources.Values)
            {
                var damageSource = source;
                damageSource.Damage *= damageModifier;
            }
        }

        public void AddDamageSource(DamageSource damageSource)
        {
            if (m_DamageSources.TryGetValue(damageSource.DamageType, out var currentSource))
            {
                currentSource.Damage += damageSource.Damage;
            }
            else
            {
                m_DamageSources.Add(damageSource.DamageType,new DamageSource(damageSource));
            }
        }

        protected virtual void ComputeSpellDamage(BoardEntity entity)
        {
            AddDamageSource(new DamageSource(m_DamageSpellParams.InitialSourceDamage));
        }

        #endregion
        //Apply Damage To All Ennemies in the actionTiles
        protected override void EntityHit(BoardEntity entity, TriggerSpellData spellData,
            Vector2Int origin, CastInfo castInfo)
        {
            base.EntityHit(entity,spellData,origin,castInfo);
            DamageEntity(entity,spellData);
            castInfo?.AddHitEntity(entity);
        }

        protected void DamageEntity(BoardEntity entity,TriggerSpellData spellData)
        {
            float totalDamage = 0;
            MainDamageType mainDamageType = m_DamageSpellParams.DamageType.MainDamageType;
            //Foreach Damage Sources//
            foreach (DamageSource damageSource in m_DamageSources.Values)
            {
                totalDamage +=
                    DamageManager.DamageStep(entity, damageSource, mainDamageType, spellData,m_DisplayDamage,m_SpellAnimDelay,m_SpellEfficiency); //DamageSource);
            }
            
            entity.EntityEvent.OnGetHitFromSpell?.Invoke(entity,this);
            entity.TakeDamage(totalDamage,spellData.AttachedEntity);

            /*Text Display */
            if (m_DisplayDamage && DamageManager.BlendDisplayDamage)
            {
                FloatingTextManager.Instance.SpawnFloatingText(entity.WorldPosition,totalDamage.ToString("0"),ColorHelper.GetDamageBlendColor(m_DamageSources),m_SpellAnimDelay);
            }
        }

        public void SetInitialDamageSource(float initialDamageSource)
        {
            m_DamageSpellParams.InitialSourceDamage.Damage = initialDamageSource;
        }

        protected override CastInfo GetCastInfo(TriggerSpellData spellData)
        {
            if (OnCastSpell != null)
            {
                return new DamageCastInfo(spellData);
            }

            return null;
        }

        public override string[] GetDescriptionParts()
        {
            if (m_DamageSources.TryGetValue(m_DamageSpellParams.InitialSourceDamage.DamageType,
                    out DamageSource initialDamageSource))
            {
                string[] description = new string[1];
                description[0] = initialDamageSource.ToDescription();
                return description;
            }

            return Array.Empty<string>();
        }
    }
}