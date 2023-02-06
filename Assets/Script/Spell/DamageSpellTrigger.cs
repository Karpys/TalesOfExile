using System.Collections.Generic;
using UnityEngine;

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
    }
    //Apply Damage To All Ennemies in the actionTiles
    public override void Trigger(SpellData spellData,List<List<Vector2Int>> actionTiles)
    {
        EntityGroup targetGroup = EntityHelper.GetInverseEntityGroup(spellData.AttachedEntity.EntityGroup);
        //Apply Damage Modifier Base on the entity damageType Modifier//
        //Make sure the modifier are precalculated//
        foreach (DamageSource source in DamageSources.Values)
        {
            source.Damage *= (spellData.AttachedEntity.EntityStats.GetDamageModifier(source.DamageType) + spellData.AttachedEntity.EntityStats.GetMainTypeModifier(DamageSpellData.DamageParameters.DamageType.MainDamageType)
                + 100) / 100;
        }
        
        //Apply Damage To All Actions Tiles//
        for (int i = 0; i < actionTiles.Count; i++)
        {
            for (int j = 0; j < actionTiles[i].Count; j++)
            {
                //Damage Entity At actionTile Pos//
                BoardEntity damageTo = MapData.Instance.GetEntityAt(actionTiles[i][j],targetGroup);
                if(!damageTo)
                    continue;
                
                //Foreach Damage Sources//
                foreach (DamageSource damageSource in DamageSources.Values)
                {
                    DamageManager.Instance.TryDamageEnnemy(damageTo, spellData.AttachedEntity,damageSource); //DamageSource);
                }
            }
        }
        return;
    }
}