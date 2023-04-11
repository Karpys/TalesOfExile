using System.Collections.Generic;
using UnityEngine;

public static class MapHelper
{
    public static void ZoneTryPlaceTile(WorldTile tilePrefab, Zone zone, Vector2Int origin, Map map)
    {
        List<Vector2Int> zonePosition = ZoneTileManager.GetSelectionZone(zone,origin,zone.Range);

        foreach (Vector2Int pos in zonePosition)
        {
            map.TryPlaceTileAt(tilePrefab, pos);
        }
    }

    public static void InsertMapInsideMap(Map originalMap, Map insertMap, Vector2Int insertOriginPosition)
    {
        foreach (Tile mapTile in insertMap.Tiles)
        {
            if(!mapTile.WorldTile)
                continue;

            originalMap.TryInsertWorldTileAt(mapTile.WorldTile, mapTile.TilePosition + insertOriginPosition);
        }
    }
}
