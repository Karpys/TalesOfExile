using UnityEngine;

public abstract class SelectionSpellTrigger:BaseSpellTrigger
{
    protected SpellAnimation OnHitAnimation = null;
    protected SpellAnimation TileHitAnimation = null;

    public SelectionSpellTrigger(BaseSpellTriggerScriptable baseScriptable)
    {
        OnHitAnimation = baseScriptable.OnHitAnimation;
        TileHitAnimation = baseScriptable.OnTileHitAnimation;
    }

    protected virtual void TileHit(Vector2Int tilePosition, TriggerSpellData spellData)
    {
        if (TileHitAnimation)
        {
            if(TileHitAnimation.BaseSpellDelay > m_SpellAnimDelay)
                m_SpellAnimDelay = TileHitAnimation.BaseSpellDelay;

            TriggerTileHitFx(MapData.Instance.GetTilePosition(tilePosition),null);
        }
    }

    protected virtual void TriggerTileHitFx(Vector3 tilePosition,Transform transform, params object[] args)
    {
        TileHitAnimation.TriggerFx(tilePosition,transform,args);
    }
    
    protected virtual void EntityHit(BoardEntity entity, TriggerSpellData spellData, EntityGroup targetGroup,
        Vector2Int spellOrigin, CastInfo castInfo)
    {
        if (OnHitAnimation)
        {
            if(OnHitAnimation.BaseSpellDelay > m_SpellAnimDelay)
                m_SpellAnimDelay = OnHitAnimation.BaseSpellDelay;

            TriggerOnHitFx(entity.WorldPosition,null);
        }
    }

    protected virtual void TriggerOnHitFx(Vector3 entityPosition,Transform transform, params object[] args)
    {
        OnHitAnimation.TriggerFx(entityPosition,transform,args);
    }
    public override void Trigger(TriggerSpellData spellData, SpellTiles spellTiles,CastInfo castInfo)
    {
        m_SpellAnimDelay = 0;
        EntityGroup targetGroup = GetEntityGroup(spellData);

        for (int i = 0; i < spellTiles.ActionTiles.Count; i++)
        {
            for (int j = 0; j < spellTiles.ActionTiles[i].Count; j++)
            {
                TileHit(spellTiles.ActionTiles[i][j],spellData);
                
                BoardEntity entityHit = MapData.Instance.GetEntityAt(spellTiles.ActionTiles[i][j],targetGroup);
                
                if(!entityHit)
                    continue;
                
                EntityHit(entityHit,spellData,targetGroup,spellTiles.OriginTiles[i],castInfo);
                //Foreach Damage Sources//
            }
        }
        
        if (spellData.AttachedEntity.EntityData.m_EntityGroup == EntityGroup.Friendly)
        {
            GameManager.Instance.FriendlyWaitTime = m_SpellAnimDelay;
        }
        else
        {
            GameManager.Instance.EnnemiesWaitTime = m_SpellAnimDelay;
        }
    }

    protected virtual EntityGroup GetEntityGroup(TriggerSpellData spellData)
    {
        return EntityHelper.GetInverseEntityGroup(spellData.AttachedEntity.EntityGroup);
    }

    public override void ComputeSpellData(BoardEntity entity)
    {
        OnCastSpell = null;
        entity.EntityEvent.OnRequestCastEvent?.Invoke(this);
    }
}