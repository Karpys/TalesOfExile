using UnityEngine;

public class TileLibrary : SingletonMonoBehavior<TileLibrary>
{
    [SerializeField] private TileKey[] m_TileKeys = null;

    public WorldTile GetTileViaKey(TileType type)
    {
        foreach (TileKey key in m_TileKeys)
        {
            if (key.Type == type)
                return key.Tile;
        }
        
        Debug.LogError("Tile Not found : " + type);
        return null;
    }
}

[System.Serializable]
public class TileKey
{
    [SerializeField] private TileType m_Type = TileType.None;
    [SerializeField] private WorldTile m_Tile = null;

    public TileType Type => m_Type;
    public WorldTile Tile => m_Tile;
}

public enum TileType
{
    None,
    IceWall,
}