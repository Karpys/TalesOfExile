using UnityEngine;

public class RushTrigger : DamageSpellTrigger
{
    public RushTrigger(DamageSpellScriptable damageSpellData) : base(damageSpellData)
    {
    }

    public override void Trigger(TriggerSpellData spellData, SpellTiles spellTiles)
    {
        base.Trigger(spellData, spellTiles);
        MoveToClosestFreeTile(spellData,spellTiles.OriginTiles[0]);
    }

    private void MoveToClosestFreeTile(TriggerSpellData spellData,Vector2Int position)
    {
        Tile closestFree = TileHelper.GetFreeClosestAround(MapData.Instance.GetTile(position),spellData.AttachedEntity.WorldPosition);
        spellData.AttachedEntity.MoveTo(closestFree.TilePosition);
    }
}