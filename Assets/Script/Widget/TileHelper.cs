
using System;
using System.Collections.Generic;
using UnityEngine;

public static class TileHelper
{
    private static Vector2Int[] DirectionalCheck = new Vector2Int[]
    {
        new Vector2Int(0, 1),
        new Vector2Int(1, 0),
        new Vector2Int(0, -1),
        new Vector2Int(-1, 0),
    };
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

    private static int GetBitMaskingValue(Tile tile,List<Tile> tiles,MapData mapData)
    {
        string bitMask = "";
        
        for (int i = DirectionalCheck.Length - 1; i >= 0; i--)
        {
            int checkX = tile.XPos + DirectionalCheck[i].x;
            int checkY = tile.YPos + DirectionalCheck[i].y;
            
            if (checkX >= 0 && checkX < mapData.Map.Width && checkY >= 0 && checkY < mapData.Map.Height)
            {
                if (tiles.Contains(mapData.Map.Tiles[checkX, checkY]))
                {
                    bitMask += "1";
                    continue;
                }
            }

            bitMask += "0";
        }

        Debug.Log(bitMask);
        return Convert.ToInt32(bitMask, 2);
    }

    public static void GenerateTileSet(List<Tile> tiles,Sprite[] tileMap,MapData mapData)
    {
        foreach (Tile tile in tiles)
        {
            Debug.Log(tile.TilePosition);
            Debug.Log(GetBitMaskingValue(tile,tiles,mapData));
            tile.GetComponentInChildren<SpriteRenderer>().sprite = tileMap[GetBitMaskingValue(tile, tiles, mapData)];
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
