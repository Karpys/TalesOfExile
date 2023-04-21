using UnityEngine;

public class RushTrigger : DamageSpellTrigger
{
    public RushTrigger(DamageSpellScriptable damageSpellData) : base(damageSpellData)
    {
    }

    public override void Trigger(TriggerSpellData spellData, SpellTiles spellTiles)
    {
        MoveToClosestFreeTile(spellData,spellTiles.OriginTiles[0]);
        base.Trigger(spellData, spellTiles);
    }

    protected override void EntityHit(BoardEntity entity, TriggerSpellData spellData, EntityGroup targetGroup, Vector2Int origin)
    {
        base.EntityHit(entity, spellData, targetGroup, origin);
        //Silence Showcase
        //Buff silenceDebuff = BuffLibrary.Instance.GetBuffViaKey(BuffType.SilenceDebuff, entity);
        //silenceDebuff.InitializeBuff(spellData.AttachedEntity,entity,10,0);
    }

    private void MoveToClosestFreeTile(TriggerSpellData spellData,Vector2Int position)
    {
        Tile closestFree = TileHelper.GetFreeClosestAround(MapData.Instance.GetTile(position),spellData.AttachedEntity.WorldPosition);
        spellData.AttachedEntity.MoveTo(closestFree.TilePosition);
    }
}