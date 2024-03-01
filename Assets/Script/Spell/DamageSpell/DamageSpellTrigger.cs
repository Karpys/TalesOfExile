using System;
using System.Collections.Generic;
using KarpysDev.Script.Entities;
using KarpysDev.Script.Manager;
using KarpysDev.Script.Utils;
using KarpysDev.Script.Widget;
using UnityEngine;

//DAMAGE SPELL TRIGGER need to inherit from BaseTargetEntitySpell abstract class//
//return a List of Entity and then here apply all the damage source//
//The animation part can be move to BaseTargetEntitySpell//
namespace KarpysDev.Script.Spell.DamageSpell
{
    using KarpysUtils;

    public class DamageSpellTrigger : SelectionSpellTrigger,IDamageProvider
    {
        protected List<DamageSource> m_BaseDamageSources = new List<DamageSource>();
        // protected DamageParameters m_DamageSpellParams = null;

        protected List<DamageSource> m_ComputedDamageSources = new List<DamageSource>();

        public List<DamageSource> ComputedDamageSources => m_ComputedDamageSources;

        private bool m_DisplayDamage = false;
        public List<DamageSource> DamageSources => m_BaseDamageSources;
        public DamageSpellTrigger(DamageSpellScriptable damageSpellData):base(damageSpellData)
        {
            if (damageSpellData.InitialBaseDamageDefaultSources == null)
            {
                return;
            }
            
            m_BaseDamageSources = damageSpellData.InitialBaseDamageDefaultSources.Init();
            //Todo: When level spell fetch change damage update this Dictionary//
        }

        public override void SetAttachedSpell(SpellData spellData, int priority)
        {
            base.SetAttachedSpell(spellData, priority);
            m_DisplayDamage = m_AttachedSpell.AttachedEntity.EntityGroup != EntityGroup.Enemy;

            if (m_BaseDamageSources.Count == 0)
            {
                m_AttachedSpell.Data.SpellName.LogError("No Damage group source in spell");
            }
        }

        #region ComputePart

    

        public override void ComputeSpellData(BoardEntity entity)
        {
            m_ComputedDamageSources.Clear();

            FloatSocket bonusDamage = new FloatSocket();
            entity.EntityEvent.OnRequestBonusSpellDamage?.Invoke(this,bonusDamage);
        
            ComputeSpellDamage(entity,bonusDamage.Value);
            ApplyDamageModifier(entity,bonusDamage.Value);
        
            //DESIGN CHOICE : Additional damage are added after the damage modifier are applied
            //additional damage scale their own way => ex FireHandBuff
            entity.EntityEvent.OnRequestAdditionalSpellSource?.Invoke(this);
            base.ComputeSpellData(entity);
        }

        private void ApplyDamageModifier(BoardEntity entity,float bonusModifier)
        {
            // float damageModifier = DamageManager.GetDamageModifier(m_DamageSpellParams, entity.EntityStats, bonusModifier);
            //Todo: Move into "ToDamageSource" => "ToComputedDamageSource" and remove this method//
            foreach (DamageSource source in m_ComputedDamageSources)
            {
                float damageModifier = bonusModifier;
                damageModifier += entity.EntityStats.GetDamageModifier(source.DamageType);
                source.PercentAmplifyBy(damageModifier);
            }
        }

        public void AddDamageSource(DamageSource damageSource)
        {
            m_ComputedDamageSources.Add(new DamageSource(damageSource));
        }

        protected virtual void ComputeSpellDamage(BoardEntity entity,float bonusDamage)
        {
            foreach (DamageSource baseDamageSource in m_BaseDamageSources)
            {
                baseDamageSource.ToDamageSource(m_ComputedDamageSources,entity,bonusDamage);
            }
            // AddDamageSource(new DamageSource(m_DamageSpellParams.InitialSourceDamage));
        }

        #endregion
        //Apply Damage To All Ennemies in the actionTiles
        protected override void EntityHit(BoardEntity entity, TriggerSpellData spellData,
            Vector2Int origin, CastInfo castInfo)
        {
            base.EntityHit(entity,spellData,origin,castInfo);
            DamageEntityStep(entity,spellData);
            castInfo?.AddHitEntity(entity);
        }

        protected virtual float DamageEntityStep(BoardEntity entity,TriggerSpellData spellData)
        {
            float totalDamage = 0;
            //Foreach Damage Sources//
            foreach (DamageSource damageSource in m_ComputedDamageSources)
            {
                totalDamage +=
                    DamageManager.DamageStep(entity, damageSource, spellData,m_DisplayDamage,m_SpellAnimDelay,m_SpellEfficiency); //DamageSource);
            }
            
            entity.EntityEvent.OnGetHitFromSpell?.Invoke(entity,this);
            entity.TakeDamage(totalDamage,spellData.AttachedEntity);

            /*Text Display */
            if (m_DisplayDamage && DamageManager.BlendDisplayDamage)
            {
                FloatingTextManager.Instance.SpawnFloatingText(entity.WorldPosition,totalDamage.ToString("0"),ColorHelper.GetDamageBlendColor(m_ComputedDamageSources),m_SpellAnimDelay);
            }

            return totalDamage;
        }

        public void SetInitialDamageSource(float initialDamageSource)
        {
            if (m_BaseDamageSources.Count <= 0)
            {
                Debug.LogError("Try set initial damage without base initial damage");
                return;
            }
            m_BaseDamageSources[0].Damage = initialDamageSource;
        }

        protected override CastInfo GetCastInfo(TriggerSpellData spellData,bool isMainCasted)
        {
            if (OnCastSpell != null)
            {
                return new DamageCastInfo(spellData,isMainCasted);
            }

            return null;
        }

        //Todo: Rework with key type like &FirstFireDamage and an interpretor
        public override string[] GetDescriptionParts()
        {
            if (m_ComputedDamageSources.Count > 0)
            {
                return m_ComputedDamageSources[0].ToDescription().ToSingleArray();
            }

            return Array.Empty<string>();
        }
    }
}