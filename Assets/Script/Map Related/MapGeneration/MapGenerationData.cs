using UnityEngine;

//Make this abstract
public abstract class MapGenerationData : ScriptableObject
{
    [Header("Base Map Data")] 
    [SerializeField] protected int m_Width = 50;
    [SerializeField] protected int m_Height = 50;
    [SerializeField] protected WorldTile m_BaseTile = null;
    
    
    protected MapData m_MapData = null;
    protected Map m_Map = null;

    public virtual GenerationMapInfo Generate(MapData mapData)
    {
        m_MapData = mapData;
        m_Map = mapData.Map;
        m_Map.Height = m_Height;
        m_Map.Width = m_Width;
        
        if(m_Map.Tiles == null)
            m_Map.Tiles = new Tile[m_Width,m_Height];
        
        for (int x = 0; x < m_Width; x++)
        {
            for (int y = 0; y < m_Height; y++)
            {
                m_Map.Tiles[x, y] = new Tile(x,y);
                m_Map.PlaceTileAt(m_BaseTile, x, y);
                OnGenerateBaseTile(x,y);
            }
        }

        return new GenerationMapInfo(new Vector2Int(0,0));
    }

    protected virtual void OnGenerateBaseTile(int x, int y)
    {
        return;
    }
    

   
}