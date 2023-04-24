using UnityEngine;

public abstract class SelectionSpellTrigger:BaseSpellTrigger
{
    protected bool AdditionalDatasAnimation = false;
    protected SpellAnimation OnHitAnimation = null;
    protected SpellAnimation TileHitAnimation = null;

    public SelectionSpellTrigger(BaseSpellTriggerScriptable baseScriptable)
    {
        OnHitAnimation = baseScriptable.OnHitAnimation;
        TileHitAnimation = baseScriptable.OnTileHitAnimation;
        AdditionalDatasAnimation = baseScriptable.AdditionalAnimDatas;
    }

    protected virtual void TileHit(Vector2Int tilePosition, TriggerSpellData spellData)
    {
        SpellAnimation tileHitAnim = TileHitAnimation;
        
        if (tileHitAnim)
        {
            if(tileHitAnim.BaseSpellDelay > m_SpellAnimDelay)
                m_SpellAnimDelay = tileHitAnim.BaseSpellDelay;

            tileHitAnim.TriggerFx(MapData.Instance.GetTilePosition(tilePosition));
        }
    }
    protected virtual void EntityHit(BoardEntity entity,TriggerSpellData spellData,EntityGroup targetGroup,Vector2Int spellOrigin)
    {
        SpellAnimation onHitAnim = OnHitAnimation;

        //Animation
        if (onHitAnim)
        {
            if(onHitAnim.BaseSpellDelay > m_SpellAnimDelay)
                m_SpellAnimDelay = onHitAnim.BaseSpellDelay;

            if (AdditionalDatasAnimation)
            {
                object[] additionalData = {spellData, entity};
                onHitAnim.TriggerFx(entity.WorldPosition,null,additionalData);
            }
            else
            {
                onHitAnim.TriggerFx(entity.WorldPosition);
            }
        }
    }
    public override void Trigger(TriggerSpellData spellData, SpellTiles spellTiles)
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
                
                EntityHit(entityHit,spellData,targetGroup,spellTiles.OriginTiles[i]);
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
        ComputeSpellPriority();
    }
}