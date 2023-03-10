using System.Collections.Generic;
using UnityEngine;

//DAMAGE SPELL TRIGGER need to inherit from BaseTargetEntitySpell abstract class//
//return a List of Entity and then here apply all the damage source//
//The animation part can be move to BaseTargetEntitySpell//
public class DamageSpellTrigger : SelectionSpellTrigger
{
    public DamageParameters DamageSpellData = null;

    protected Dictionary<SubDamageType, DamageSource> DamageSources = new Dictionary<SubDamageType, DamageSource>();
    //public void ComputeAdditional Sources//
    public DamageSpellTrigger(DamageSpellScriptable damageSpellData):base(damageSpellData)
    {
        Debug.Log("Call Base Damage Spell Trigger");
        DamageSpellData = new DamageParameters(damageSpellData.BaseDamageParameters);
    }

    public override void ComputeSpellData(BoardEntity entity)
    {
        //Compute Spell Damage
        DamageSources.Clear();
        
        DamageSources.Add(DamageSpellData.InitialSourceDamage.DamageType,new DamageSource(DamageSpellData.InitialSourceDamage));
        DamageSource[] additionalSources = entity.GetAdditionalSources(DamageSpellData.DamageType);
        
        for (int i = 0; i < additionalSources.Length; i++)
        {
            DamageSource currentSource = null;
            if (DamageSources.TryGetValue(additionalSources[i].DamageType, out currentSource))
            {
                currentSource.Damage += additionalSources[i].Damage;
            }
            else
            {
                DamageSources.Add(additionalSources[i].DamageType,new DamageSource(additionalSources[i]));
            }
        }
        
        foreach (DamageSource source in DamageSources.Values)
        {
            source.Damage *= (entity.EntityStats.GetDamageModifier(source.DamageType) + entity.EntityStats.GetMainTypeModifier(DamageSpellData.DamageType.MainDamageType)
                + 100) / 100;
        }
    }
    //Apply Damage To All Ennemies in the actionTiles
    
    public override void Trigger(TriggerSpellData spellData,SpellTiles spellTiles)
    {
        base.Trigger(spellData,spellTiles);
    }
    
    protected override void TileHit(Vector2Int tilePosition,TriggerSpellData spellData)
    {
        base.TileHit(tilePosition, spellData);
    }

    protected override void EntityHit(BoardEntity entity,TriggerSpellData spellData,EntityGroup targetGroup,Vector2Int origin)
    {
        base.EntityHit(entity,spellData,targetGroup,origin);
        
        float totalDamage = 0;
        //Foreach Damage Sources//
        foreach (DamageSource damageSource in DamageSources.Values)
        {
            totalDamage += DamageManager.Instance.TryDamageEnnemy(entity, spellData.AttachedEntity,damageSource); //DamageSource);
        }
        
        /*Text Display */
        if (targetGroup == EntityGroup.Ennemy)
        {
            FloatingTextManager.Instance.SpawnFloatingText(entity.WorldPosition,-totalDamage,ColorHelper.GetDamageBlendColor(DamageSources),m_SpellAnimDelay);
        }
    }

}

public class LeapCrashTrigger : DamageSpellTrigger
{
    public LeapCrashTrigger(DamageSpellScriptable damageSpellData) : base(damageSpellData)
    {
    }

    protected override void TileHit(Vector2Int tilePosition, TriggerSpellData spellData)
    {
        if(tilePosition == spellData.AttachedEntity.EntityPosition)
            return;
        
        base.TileHit(tilePosition, spellData);
    }

    public override void Trigger(TriggerSpellData spellData, SpellTiles spellTiles)
    {
        spellData.AttachedEntity.MoveTo(spellTiles.OriginTiles[0]);
        base.Trigger(spellData, spellTiles);
    }
}