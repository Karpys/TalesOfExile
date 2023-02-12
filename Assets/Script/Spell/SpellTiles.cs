using System.Collections.Generic;
using UnityEngine;

public class SpellTiles
{
    public List<Vector2Int> OriginTiles = new List<Vector2Int>();
    public List<List<Vector2Int>> ActionTiles = new List<List<Vector2Int>>();

    public SpellTiles(List<Vector2Int> originTiles, List<List<Vector2Int>> actionTiles)
    {
        OriginTiles = originTiles;
        ActionTiles = actionTiles;
    }
}
