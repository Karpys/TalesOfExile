using System.Collections.Generic;
using UnityEngine;

public class SpellTiles
{
    public Vector2Int CenterOrigin = Vector2Int.zero;
    public List<Vector2Int> OriginTiles = new List<Vector2Int>();
    public List<List<Vector2Int>> ActionTiles = new List<List<Vector2Int>>();

    public SpellTiles(Vector2Int centerOrigin,List<Vector2Int> originTiles, List<List<Vector2Int>> actionTiles)
    {
        CenterOrigin = centerOrigin;
        OriginTiles = originTiles;
        ActionTiles = actionTiles;
    }
}
