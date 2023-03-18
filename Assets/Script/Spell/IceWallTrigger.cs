using UnityEngine;

public class IceWallTrigger : SelectionSpellTrigger
{
    public IceWallTrigger(BaseSpellTriggerScriptable baseScriptable) : base(baseScriptable)
    {
    }

    public override void ComputeSpellPriority()
    {
        m_SpellPriority = 0;
    }

    protected override void TileHit(Vector2Int tilePosition, TriggerSpellData spellData)
    {
        base.TileHit(tilePosition, spellData);
        
        if(!MapData.Instance.IsWalkable(tilePosition))
            return;

        WorldTile tile = TileLibrary.Instance.GetTileViaKey(TileType.IceWall);
        MapData.Instance.Map.PlaceTileAt(tile, tilePosition.x, tilePosition.y);
    }
}