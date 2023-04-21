using UnityEngine;

public class IceWallTrigger : SelectionSpellTrigger
{
    private TileType m_TileType = TileType.None;
    
    public IceWallTrigger(BaseSpellTriggerScriptable baseScriptable,TileType tileType) : base(baseScriptable)
    {
        m_TileType = tileType;
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

        WorldTile tile = TileLibrary.Instance.GetTileViaKey(m_TileType);
        MapData.Instance.Map.PlaceTileAt(tile, tilePosition.x, tilePosition.y);
    }
}