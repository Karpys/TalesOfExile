using System.Collections.Generic;
using TweenCustom;
using UnityEngine;

public class KnockBackTrigger : DamageSpellTrigger
{
    private List<BoardEntity> m_EntityHits = new List<BoardEntity>();
    private int m_RepulseForce = 2;

    private TriggerSpellData m_RangeAutoTrigger = null;
    
    public KnockBackTrigger(DamageSpellScriptable damageSpellData, int repulseForce) : base(damageSpellData)
    {
        m_RepulseForce = repulseForce;
    }
    
    public override void Trigger(TriggerSpellData spellData, SpellTiles spellTiles)
    {
        m_EntityHits.Clear();
        m_RangeAutoTrigger = spellData.AttachedEntity.GetSpellViaKey("RangeAuto") as TriggerSpellData;
        base.Trigger(spellData, spellTiles);
    }

    protected override void EntityHit(BoardEntity entity, TriggerSpellData spellData, EntityGroup targetGroup,Vector2Int origin)
    {
        if (m_EntityHits.Contains(entity))
        {
            UpdateEntityPosition(entity);   
            return;
        }
        
        m_EntityHits.Add(entity);

        for (int i = 0; i < m_RepulseForce; i++)
        {
            Debug.Log("Entity position :" + entity.EntityPosition);
            Vector2Int opposite = TileHelper.GetOppositePositionFrom(entity.EntityPosition, spellData.AttachedEntity.EntityPosition);
            
            
            if (MapData.Instance.IsWalkable(opposite))
            {
                entity.MoveTo(opposite,false);
                m_SpellAnimDelay = 0.1f;
            }
            else
            {
                base.EntityHit(entity, spellData, targetGroup,origin);
                break;
            }
        }
        
        if(m_RangeAutoTrigger != null)
            SpellCastUtils.CastSpellAt(m_RangeAutoTrigger,entity.EntityPosition,spellData.AttachedEntity.EntityPosition,true);
        
        UpdateEntityPosition(entity);
    }

    private void UpdateEntityPosition(BoardEntity entity)
    {
        entity.transform.DoMove(MapData.Instance.GetTilePosition(entity.EntityPosition), 0.1f);
    }
        
}