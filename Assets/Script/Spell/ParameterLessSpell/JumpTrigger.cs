using UnityEngine;

public class JumpTrigger : SelectionSpellTrigger
{
    protected override void TileHit(Vector2Int tilePosition,TriggerSpellData spellData)
    {
        //Move the entity//
        spellData.AttachedEntity.MoveTo(tilePosition.x,tilePosition.y);
    }

    protected override void EntityHit(BoardEntity entity, TriggerSpellData spellData, EntityGroup targetGroup,Vector2Int origin)
    {
        return;
    }
}