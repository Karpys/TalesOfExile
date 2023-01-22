using UnityEngine;

public class MapData : MonoBehaviour
{
    [SerializeField] private MapDataLibrary m_MapDataLibrary = null;
    private Map m_Map = null;

    public Map Map => m_Map;
    
    public Vector3 GetTilePosition(int x, int y)
    {
        return new Vector3(x * m_MapDataLibrary.TileSize, y * m_MapDataLibrary.TileSize, 0);
    }
}