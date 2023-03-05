using System.Collections.Generic;
using TweenCustom;
using UnityEngine;

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