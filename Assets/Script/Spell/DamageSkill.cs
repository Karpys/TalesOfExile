using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Basic Damage Spell", menuName = "New Spell Trigger", order = 0)]
public class DamageSkill : BaseSpellTriggerScriptable
{
    [SerializeField] private DamageParameters m_DamageParameters = null;
    public override void Trigger(SpellData spellData,List<List<Vector2Int>> actionTiles)
    {
        //Compute Spell DamageSources and Damage Values//
        List<DamageSource> damageSources = new List<DamageSource>();
        damageSources.Add(new DamageSource(m_DamageParameters.InitialSourceDamage));
        
        //Todo//
        //Get Additional damageSources//
        //AdditionnalSources = spellData.AttachedEntity.GetAdditionnalSources(m_DamageParameters.MainDamageType,m_DamageParameters.SubTypes)//
        //foreachAdditionalSources
        DamageSource[] additionalSources = spellData.AttachedEntity.GetAdditionalSources(m_DamageParameters.MainDamageType,m_DamageParameters.SubDamageTypes);

        for (int i = 0; i < additionalSources.Length; i++)
        {
            bool contain = false;
            for (int j = 0; j < damageSources.Count; j++)
            {
                if (damageSources[j].DamageType == additionalSources[i].DamageType)
                {
                    damageSources[j].Damage += additionalSources[i].Damage;
                    contain = true;
                }
            }
            
            if(!contain)
                damageSources.Add(new DamageSource(additionalSources[i]));
        }
        //Apply Damage Modifier Base on the entity damageType Modifier//
        //Make sure the modifier are precalculated//
        foreach (DamageSource source in damageSources)
        {
            source.Damage *= (spellData.AttachedEntity.EntityStats.GetDamageModifier(source.DamageType) + spellData.AttachedEntity.EntityStats.GetMainTypeModifier(m_DamageParameters.MainDamageType)
                              + 100) / 100;
        }
        
        //Apply Damage To All Actions Tiles//
        for (int i = 0; i < actionTiles.Count; i++)
        {
            for (int j = 0; j < actionTiles[i].Count; j++)
            {
                //Damage Entity At actionTile Pos//
                BoardEntity damageTo = MapData.Instance.GetEntityAt(actionTiles[i][j]);
                if(!damageTo)
                    continue;
                
                //Foreach Damage Sources//
                for (int h = 0; h < damageSources.Count; h++)
                {
                    DamageManager.Instance.TryDamageEnnemy(damageTo, spellData.AttachedEntity,damageSources[h]); //DamageSource);
                }
            }
        }
        return;
    }
}

[System.Serializable]
public class DamageParameters
{
    public MainDamageType MainDamageType = MainDamageType.Melee;
    public float WeaponAdditionalDamagePercentage = 0;
    public SubDamageType[] SubDamageTypes = new SubDamageType[1];
    public DamageSource InitialSourceDamage = null;
}

[System.Serializable]
public class DamageSource
{
    public float Damage = 10;
    public SubDamageType DamageType = SubDamageType.Physical;

    public DamageSource(float damage, SubDamageType damageType)
    {
        Damage = damage;
        DamageType = damageType;
    }

    public DamageSource(DamageSource baseDamageSource)
    {
        Damage = baseDamageSource.Damage;
        DamageType = baseDamageSource.DamageType;
    }
}

public enum MainDamageType
{
    Melee,
    Projectile,
    Spell,
}

public enum SubDamageType
{
    Physical,//Base Weapon Damage//
    Elemental,
    Fire,
    Cold,
    Holy,
    //Ect ect
}