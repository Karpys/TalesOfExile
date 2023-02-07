using System;
using System.Collections.Generic;
using UnityEngine;

public class MapData : SingletonMonoBehavior<MapData>
{
    [SerializeField] private MapDataLibrary m_MapDataLibrary = null;
    private Map m_Map = null;
    public Map Map
    {
        get { return m_Map; }
        set { m_Map = value; }
    }
    
    
    public bool IsWalkable(int x, int y)
    {
        if (x >= 0 && x < m_Map.Width && y >= 0 && y < m_Map.Height && m_Map.Tiles[x,y].Walkable)
        {
            return true;
        }
        return false;
    }

    public bool IsWalkable(Vector2Int pos)
    {
        return IsWalkable(pos.x, pos.y);
    }

    public Vector3 GetTilePosition(int x, int y)
    {
        return new Vector3(x * m_MapDataLibrary.TileSize, y * m_MapDataLibrary.TileSize, 0);
    }

    public Vector2Int GetPlayerPosition()
    {
        return GameManager.Instance.Player.EntityPosition;
    }
    
    public BoardEntity GetEntityAt(Vector2Int entityPos,EntityGroup targetEntityGroup)
    {
        List<BoardEntity> boardEntities = new List<BoardEntity>();

        //Get the selected group list
        if (targetEntityGroup == EntityGroup.Ennemy)
        {
            boardEntities = GameManager.Instance.EnnemiesOnBoard;
        }
        else if(targetEntityGroup == EntityGroup.Friendly)
        {
            boardEntities = GameManager.Instance.FriendlyOnBoard;
        }
        
        for (int i = 0; i < boardEntities.Count; i++)
        {
            if (boardEntities[i].EntityPosition == entityPos)
                return boardEntities[i];
        }
        
        return null;
    }
}