
using System.Collections.Generic;
using UnityEngine;

public static class TileHelper
{
    public static List<Tile> GetNeighbours(Tile tile,NeighbourType type,MapData mapData)
    {
        List<Tile> neighbours = new List<Tile>();

        if (type == NeighbourType.Square)
        {
            GetSquareNeighbours(tile,neighbours,mapData);
        }else if (type == NeighbourType.Cross)
        {
            GetCrossNeighbours(tile,neighbours,mapData);
        }
        

        return neighbours;
    }

    private static void GetCrossNeighbours(Tile tile, List<Tile> neighbours,MapData mapData)
    {
        for (int x = -1; x <= 1; x++)
        {
            if(x == 0)
                continue;
            
            int checkX = tile.XPos + x;
            if (checkX >= 0 && checkX < mapData.Map.Width)
            {
                neighbours.Add(mapData.Map.Tiles[checkX,tile.YPos]);
            }
        }
        
        for (int y = -1; y <= 1; y++)
        {
            if(y == 0)
                continue;
            
            int checkY = tile.YPos + y;
            if (checkY >= 0 && checkY < mapData.Map.Height)
            {
                neighbours.Add(mapData.Map.Tiles[tile.XPos,checkY]);
            }
        }
    }

    private static void GetSquareNeighbours(Tile tile, List<Tile> neighbours,MapData mapData)
    {
        for (int x = -1; x <= 1; x++)
        {
            for (int y = -1; y <= 1; y++)
            {
                if(x == 0 && y == 0)
                    continue;

                int checkX = tile.XPos + x;
                int checkY = tile.YPos + y;

                if (checkX >= 0 && checkX < mapData.Map.Width && checkY >= 0 && checkY < mapData.Map.Height)
                {
                    neighbours.Add(mapData.Map.Tiles[checkX,checkY]); 
                }
            }
        }
    }
    
    public static List<Vector2Int> ToPath(this List<Tile> tiles)
    {
        List<Vector2Int> path = new List<Vector2Int>();

        for (int i = 0; i < tiles.Count; i++)
        {
            path.Add(tiles[i].TilePosition);
        }

        return path;
    }
}
