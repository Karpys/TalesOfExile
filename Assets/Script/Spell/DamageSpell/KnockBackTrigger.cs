using System.Collections.Generic;
using TweenCustom;
using UnityEngine;

public class KnockBackTrigger : DamageSpellTrigger
{
    private List<BoardEntity> m_EntityHits = new List<BoardEntity>();
    private int m_RepulseForce = 2;

    
    
    public KnockBackTrigger(DamageSpellScriptable damageSpellData) : base(damageSpellData){}
    
    public KnockBackTrigger(DamageSpellScriptable damageSpellData, string repulseForce) : base(damageSpellData)
    {
        m_RepulseForce = int.Parse(repulseForce);
    }
    
    public override void Trigger(TriggerSpellData spellData, SpellTiles spellTiles)
    {
        m_EntityHits.Clear();
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

        UpdateEntityPosition(entity);
    }

    private void UpdateEntityPosition(BoardEntity entity)
    {
        entity.transform.DoMove(MapData.Instance.GetTilePosition(entity.EntityPosition), 0.1f);
    }
        
}