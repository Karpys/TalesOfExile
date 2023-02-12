using System.Collections.Generic;
using UnityEngine;

//DAMAGE SPELL TRIGGER need to inherit from BaseTargetEntitySpell abstract class//
//return a List of Entity and then here apply all the damage source//
//The animation part can be move to BaseTargetEntitySpell//
public class DamageSpellTrigger : BaseSpellTrigger
{
    public DamageSpellScriptable DamageSpellData = null;

    private Dictionary<SubDamageType, DamageSource> DamageSources = new Dictionary<SubDamageType, DamageSource>();
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
        EntityGroup targetGroup = EntityHelper.GetInverseEntityGroup(spellData.AttachedEntity.EntityGroup);
        //OnHit Spell Animation//
        float spellAnimDelay = 0;
        SpellAnimation onHitAnim = spellData.m_Data.m_OnHitAnimation;
        
        //Apply Damage To All Actions Tiles//
        for (int i = 0; i < spellTiles.ActionTiles.Count; i++)
        {
            for (int j = 0; j < spellTiles.ActionTiles[i].Count; j++)
            {
                float totalDamage = 0;
                //Damage Entity At actionTile Pos//
                BoardEntity damageTo = MapData.Instance.GetEntityAt(spellTiles.ActionTiles[i][j],targetGroup);
                
                if(!damageTo)
                    continue;
                
                //Foreach Damage Sources//
                foreach (DamageSource damageSource in DamageSources.Values)
                {
                    totalDamage += DamageManager.Instance.TryDamageEnnemy(damageTo, spellData.AttachedEntity,damageSource); //DamageSource);
                }

                //Animation And Damage Display
                if (spellData.m_Data.m_OnHitAnimation)
                {
                    spellAnimDelay = onHitAnim.BaseSpellDelay;   
                    onHitAnim.TriggerFx(Vector3.zero,damageTo.transform);
                }

                /*Text Display */
                if (targetGroup == EntityGroup.Ennemy)
                {
                    FloatingTextManager.Instance.SpawnFloatingText(damageTo.transform,-totalDamage,ColorHelper.GetDamageBlendColor(DamageSources),spellAnimDelay);
                }
            }
        }

        //Wait time
        if (spellData.AttachedEntity.m_EntityData.m_EntityGroup == EntityGroup.Friendly)
        {
            GameManager.Instance.FriendlyWaitTime = spellAnimDelay;
        }
        else
        {
            GameManager.Instance.EnnemiesWaitTime = spellAnimDelay;
        }
        return;
    }
}