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
}
