using System.Collections.Generic;
using UnityEngine;

public class MapData : SingletonMonoBehavior<MapData>
{
    [SerializeField] private MapDataLibrary m_MapDataLibrary = null;
    private Map m_Map = null;
    private PlayerBoardEntity m_PlayerCharacter = null;
    public Map Map
    {
        get { return m_Map; }
        set { m_Map = value; }
    }

    public void InitializeMapData(PlayerBoardEntity playerReference)
    {
        m_PlayerCharacter = playerReference;
    }

    public Vector3 GetTilePosition(int x, int y)
    {
        return new Vector3(x * m_MapDataLibrary.TileSize, y * m_MapDataLibrary.TileSize, 0);
    }

    public Vector2Int GetPlayerPosition()
    {
        return m_PlayerCharacter.EntityPosition;
    }
}