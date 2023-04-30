using System.Collections.Generic;
using UnityEngine;

public class LightningSpellTrigger : DamageSpellTrigger
{
    private Zone m_ZoneStrike = null;
    private int m_StrikeCount = 0;
    public LightningSpellTrigger(DamageSpellScriptable damageSpellData,ZoneType zoneType,int zoneRange,int strikeCount) : base(damageSpellData)
    {
        m_ZoneStrike = new Zone(zoneType, zoneRange);
        m_StrikeCount = strikeCount;
    }

    protected override void EntityHit(BoardEntity entity, TriggerSpellData spellData, EntityGroup targetGroup, Vector2Int origin)
    {
        base.EntityHit(entity, spellData, targetGroup, origin);

        List<BoardEntity> targetEntity = GameManager.Instance.GetEntityViaGroup(targetGroup);
        List<BoardEntity> entityStriked = new List<BoardEntity>();
        
        targetEntity.Remove(entity);

        if (targetEntity.Count == 0)
            return;

        BoardEntity lastStriked = entity;
        
        for (int i = 0; i < m_StrikeCount; i++)
        {
            if(targetEntity.Count <= 0)
                break;
            
            BoardEntity closestEntity = EntityHelper.GetClosestEntity(targetEntity,lastStriked.EntityPosition);
            
            if(!ZoneTileManager.IsInRange(lastStriked.EntityPosition,closestEntity.EntityPosition,m_ZoneStrike))
                break;

            entityStriked.Add(closestEntity);
            targetEntity.Remove(closestEntity);
            lastStriked = closestEntity;
        }


        for (int i = 0; i < entityStriked.Count; i++)
        {
            BoardEntity en = entityStriked[i];
            if (en != null)
            {
                TriggerOnHitFx(en, spellData);
                DamageEntity(en,spellData,targetGroup);
            }
        }
    }
}