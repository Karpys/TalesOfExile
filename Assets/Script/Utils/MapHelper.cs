using System.Collections.Generic;
using KarpysDev.Script.Manager.Library;
using KarpysDev.Script.Map_Related;
using KarpysDev.Script.Spell;
using UnityEngine;

namespace KarpysDev.Script.Utils
{
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
            
            for (int x = 0; x < insertMap.Width; x++)
            {
                for (int y = 0; y < insertMap.Height; y++)
                {
                    Tile tile = insertMap.Tiles[x][y];
                    
                    if(!tile.WorldTile)
                        continue;

                    originalMap.TryInsertWorldTileAt(tile.WorldTile, tile.TilePosition + insertOriginPosition);
                }
            }
        }

        public static MapPlaceable InsertMapPlaceable(PlaceableType placeableType)
        {
            return GameObject.Instantiate(PlaceableLibrary.Instance.GetViaKey(placeableType), MapData.Instance.transform);
        }
    }
}
