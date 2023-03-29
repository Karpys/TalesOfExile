using UnityEngine;
using UnityEngine.Assertions.Must;

[CreateAssetMenu(menuName = "Map/Blight", fileName = "BlightMapGeneration", order = 0)]
public class BlightMapGeneration : MapGenerationData
{
    [SerializeField] private BlightTile m_BlightCore = null;

    private BlightTile m_BlightCoreGenerated = null;
    protected override void OnGenerateBaseTile(int x, int y)
    {
        if (x == m_Width / 2 && y == m_Height / 2)
        {
            BlightTile blightTile = (BlightTile)m_Map.PlaceTileAt(m_BlightCore,x,y);
            m_BlightCoreGenerated = blightTile;
        }
        
        base.OnGenerateBaseTile(x, y);
    }

    public override GenerationMapInfo Generate(MapData mapData)
    {
        GenerationMapInfo info = base.Generate(mapData);
        m_BlightCoreGenerated.Initalize(m_Map);
        return info;
    }
}