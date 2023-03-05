using System.Collections.Generic;
using TweenCustom;
using UnityEngine;

//DAMAGE SPELL TRIGGER need to inherit from BaseTargetEntitySpell abstract class//
//return a List of Entity and then here apply all the damage source//
//The animation part can be move to BaseTargetEntitySpell//
public class DamageSpellTrigger : SelectionSpellTrigger
{
    private SpellAnimation OnHitAnimation = null;
    
    public DamageParameters DamageSpellData = null;

    protected Dictionary<SubDamageType, DamageSource> DamageSources = new Dictionary<SubDamageType, DamageSource>();
    //public void ComputeAdditional Sources//
    public DamageSpellTrigger(DamageSpellScriptable damageSpellData)
    {
        Debug.Log("Call Base Damage Spell Trigger");
        OnHitAnimation = damageSpellData.OnHitAnimation;
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
        return;
    }

    protected override void EntityHit(BoardEntity entity,TriggerSpellData spellData,EntityGroup targetGroup,Vector2Int origin)
    {
        SpellAnimation onHitAnim = OnHitAnimation;
        
        float totalDamage = 0;
        //Foreach Damage Sources//
        foreach (DamageSource damageSource in DamageSources.Values)
        {
            totalDamage += DamageManager.Instance.TryDamageEnnemy(entity, spellData.AttachedEntity,damageSource); //DamageSource);
        }

        //Animation And Damage Display
        if (OnHitAnimation)
        {
            if(onHitAnim.BaseSpellDelay > m_SpellAnimDelay)
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

public class KnockBackTrigger : DamageSpellTrigger
{
    private List<BoardEntity> m_EntityHits = new List<BoardEntity>();
    public KnockBackTrigger(DamageSpellScriptable damageSpellData) : base(damageSpellData)
    {}

    public override void Trigger(TriggerSpellData spellData, SpellTiles spellTiles)
    {
        m_EntityHits.Clear();
        base.Trigger(spellData, spellTiles);
    }

    protected override void EntityHit(BoardEntity entity, TriggerSpellData spellData, EntityGroup targetGroup,Vector2Int origin)
    {
        if(m_EntityHits.Contains(entity))
            return;
        
        m_EntityHits.Add(entity);
        Vector2Int opposite = TileHelper.GetOppositePositionFrom(entity.EntityPosition, spellData.AttachedEntity.EntityPosition);
        
        if (MapData.Instance.IsWalkable(opposite))
        {
            entity.MoveTo(opposite,false);
            entity.transform.DoMove(MapData.Instance.GetTilePosition(entity.EntityPosition),0.1f);
            m_SpellAnimDelay = 0.1f;
        }
        else
        {
            base.EntityHit(entity, spellData, targetGroup,origin);
        }
    }
}