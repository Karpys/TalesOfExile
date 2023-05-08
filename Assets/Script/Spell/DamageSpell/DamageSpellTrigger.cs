using System.Collections.Generic;
using UnityEngine;

//DAMAGE SPELL TRIGGER need to inherit from BaseTargetEntitySpell abstract class//
//return a List of Entity and then here apply all the damage source//
//The animation part can be move to BaseTargetEntitySpell//
public class DamageSpellTrigger : SelectionSpellTrigger
{
    public DamageParameters DamageSpellData = null;

    protected Dictionary<SubDamageType, DamageSource> DamageSources = new Dictionary<SubDamageType, DamageSource>();
    public DamageSpellTrigger(DamageSpellScriptable damageSpellData):base(damageSpellData)
    {
        Debug.Log("Call Base Damage Spell Trigger");
        DamageSpellData = new DamageParameters(damageSpellData.BaseDamageParameters);
    }

    #region ComputePart

    

    public override void ComputeSpellData(BoardEntity entity)
    {
        DamageSources.Clear();

        FloatSocket bonusModifier = new FloatSocket();
        entity.EntityEvent.OnRequestSpellDamage?.Invoke(this,bonusModifier);
        
        ComputeSpellDamage(entity);
        ApplyDamageModifier(entity,bonusModifier.Value);
        
        base.ComputeSpellData(entity);
    }

    private void ApplyDamageModifier(BoardEntity entity,float bonusModifier)
    {
        float damageModifier = entity.EntityStats.GetDamageParametersModifier(DamageSpellData, bonusModifier);
        
        foreach (DamageSource source in DamageSources.Values)
        {
            source.Damage *= damageModifier;
        }
    }

    public void AddDamageSource(DamageSource damageSource)
    {
        DamageSource currentSource = null;
        if (DamageSources.TryGetValue(damageSource.DamageType, out currentSource))
        {
            currentSource.Damage += damageSource.Damage;
        }
        else
        {
            DamageSources.Add(damageSource.DamageType,new DamageSource(damageSource));
        }
    }

    protected virtual void ComputeSpellDamage(BoardEntity entity)
    {
        AddDamageSource(new DamageSource(DamageSpellData.InitialSourceDamage));
    }

    #endregion
    //Apply Damage To All Ennemies in the actionTiles
    protected override void EntityHit(BoardEntity entity, TriggerSpellData spellData, EntityGroup targetGroup,
        Vector2Int origin, CastInfo castInfo)
    {
        base.EntityHit(entity,spellData,targetGroup,origin,castInfo);
        DamageEntity(entity,spellData,targetGroup);
    }

    protected void DamageEntity(BoardEntity entity,TriggerSpellData spellData,EntityGroup targetGroup)
    {
        float totalDamage = 0;
        MainDamageType mainDamageType = DamageSpellData.DamageType.MainDamageType;
        //Foreach Damage Sources//
        foreach (DamageSource damageSource in DamageSources.Values)
        {
            totalDamage += DamageManager.Instance.TryDamageEnemy(entity, spellData.AttachedEntity,damageSource,mainDamageType,spellData); //DamageSource);
        }
        
        entity.TakeDamage(totalDamage);

        /*Text Display */
        if (targetGroup == EntityGroup.Enemy)
        {
            FloatingTextManager.Instance.SpawnFloatingText(entity.WorldPosition,totalDamage,ColorHelper.GetDamageBlendColor(DamageSources),m_SpellAnimDelay);
        }
    }

    public void SetInitialDamageSource(float initialDamageSource)
    {
        DamageSpellData.InitialSourceDamage.Damage = initialDamageSource;
    }
}