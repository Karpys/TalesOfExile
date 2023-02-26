using System.Collections.Generic;
using UnityEngine;

//DAMAGE SPELL TRIGGER need to inherit from BaseTargetEntitySpell abstract class//
//return a List of Entity and then here apply all the damage source//
//The animation part can be move to BaseTargetEntitySpell//
public class DamageSpellTrigger : SelectionSpellTrigger
{
    public DamageSpellScriptable DamageSpellData = null;

    protected Dictionary<SubDamageType, DamageSource> DamageSources = new Dictionary<SubDamageType, DamageSource>();
    //public void ComputeAdditional Sources//
    public DamageSpellTrigger(DamageSpellScriptable damageSpellData)
    {
        DamageSpellData = damageSpellData;
    }

    public override void ComputeSpellData(BoardEntity entity)
    {
        //Compute Spell Damage
        DamageSources.Clear();
        
        DamageSources.Add(DamageSpellData.DamageParameters.InitialSourceDamage.DamageType,new DamageSource(DamageSpellData.DamageParameters.InitialSourceDamage));
        DamageSource[] additionalSources = entity.GetAdditionalSources(DamageSpellData.DamageParameters.DamageType);
        
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
            source.Damage *= (entity.EntityStats.GetDamageModifier(source.DamageType) + entity.EntityStats.GetMainTypeModifier(DamageSpellData.DamageParameters.DamageType.MainDamageType)
                + 100) / 100;
        }
    }
    //Apply Damage To All Ennemies in the actionTiles
    
    public override void Trigger(SpellData spellData,SpellTiles spellTiles)
    {
        base.Trigger(spellData,spellTiles);
    }
    
    protected override void TileHit(Vector2Int tilePosition)
    {
        return;
    }

    protected override void EntityHit(BoardEntity entity,SpellData spellData,EntityGroup targetGroup)
    {
        SpellAnimation onHitAnim = DamageSpellData.OnHitAnimation;
        
        float totalDamage = 0;
        //Foreach Damage Sources//
        foreach (DamageSource damageSource in DamageSources.Values)
        {
            totalDamage += DamageManager.Instance.TryDamageEnnemy(entity, spellData.AttachedEntity,damageSource); //DamageSource);
        }

        //Animation And Damage Display
        if (DamageSpellData.OnHitAnimation)
        {
            m_SpellAnimDelay = onHitAnim.BaseSpellDelay;   
            onHitAnim.TriggerFx(entity.WorldPosition);
        }

        /*Text Display */
        if (targetGroup == EntityGroup.Ennemy)
        {
            FloatingTextManager.Instance.SpawnFloatingText(entity.transform,-totalDamage,ColorHelper.GetDamageBlendColor(DamageSources),m_SpellAnimDelay);
        }
    }

}