﻿using UnityEngine;

public abstract class SelectionSpellTrigger:BaseSpellTrigger
{
    protected abstract void TileHit(Vector2Int tilePosition,TriggerSpellData spellData);
    protected abstract void EntityHit(BoardEntity entity,TriggerSpellData spellData,EntityGroup targetGroup,Vector2Int spellOrigin);
    public override void Trigger(TriggerSpellData spellData, SpellTiles spellTiles)
    {
        m_SpellAnimDelay = 0;
        EntityGroup targetGroup = EntityHelper.GetInverseEntityGroup(spellData.AttachedEntity.EntityGroup);

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

    public override void ComputeSpellData(BoardEntity entity)
    {
        
    }
}