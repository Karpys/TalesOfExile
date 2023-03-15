using UnityEngine;

public class JumpTrigger : SelectionSpellTrigger
{
    public JumpTrigger(BaseSpellTriggerScriptable baseScriptable) : base(baseScriptable)
    {
    }
    protected override void TileHit(Vector2Int tilePosition,TriggerSpellData spellData)
    {
        base.TileHit(tilePosition,spellData);
        //Move the entity//
        spellData.AttachedEntity.MoveTo(tilePosition.x,tilePosition.y);
    }

    protected override void EntityHit(BoardEntity entity, TriggerSpellData spellData, EntityGroup targetGroup,Vector2Int origin)
    {
        base.EntityHit(entity,spellData,targetGroup,origin);
        return;
    }

    public override void ComputeSpellPriority()
    {
        m_SpellPriority = 0;
    }
}