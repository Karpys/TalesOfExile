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

        public Dictionary<SubDamageType, DamageSource> DamageSource => m_DamageSources;
        public DamageSpellTrigger(DamageSpellScriptable damageSpellData):base(damageSpellData)
        {
            Debug.Log("Call Base Damage Spell Trigger");
            m_DamageSpellParams = new DamageParameters(damageSpellData.BaseDamageParameters);
        }

        #region ComputePart

    

        public override void ComputeSpellData(BoardEntity entity)
        {
            m_DamageSources.Clear();

            FloatSocket bonusModifier = new FloatSocket();
            entity.EntityEvent.OnRequestSpellDamage?.Invoke(this,bonusModifier);
        
            ComputeSpellDamage(entity);
            ApplyDamageModifier(entity,bonusModifier.Value);
        
            base.ComputeSpellData(entity);
        }

        private void ApplyDamageModifier(BoardEntity entity,float bonusModifier)
        {
            float damageModifier = entity.EntityStats.GetDamageParametersModifier(m_DamageSpellParams, bonusModifier);
        
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
        protected override void EntityHit(BoardEntity entity, TriggerSpellData spellData, EntityGroup targetGroup,
            Vector2Int origin, CastInfo castInfo)
        {
            base.EntityHit(entity,spellData,targetGroup,origin,castInfo);
            DamageEntity(entity,spellData,targetGroup);
            castInfo?.AddHitEntity(entity);
        }

        protected void DamageEntity(BoardEntity entity,TriggerSpellData spellData,EntityGroup targetGroup)
        {
            float totalDamage = 0;
            MainDamageType mainDamageType = m_DamageSpellParams.DamageType.MainDamageType;
            //Foreach Damage Sources//
            foreach (DamageSource damageSource in m_DamageSources.Values)
            {
                totalDamage +=
                    DamageManager.Instance.TryDamageEnemy(entity, damageSource, mainDamageType, spellData); //DamageSource);
            }
        
            entity.EntityEvent.OnGetHitFromSpell?.Invoke(entity,this);

            entity.TakeDamage(totalDamage);

            /*Text Display */
            if (targetGroup == EntityGroup.Enemy)
            {
                FloatingTextManager.Instance.SpawnFloatingText(entity.WorldPosition,totalDamage,ColorHelper.GetDamageBlendColor(m_DamageSources),m_SpellAnimDelay);
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
    }
}