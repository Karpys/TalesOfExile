using UnityEngine;

[CreateAssetMenu(menuName = "Map/Blight", fileName = "BlightMapGeneration", order = 0)]
public class BlightMapGeneration : MapGenerationData
{
    [SerializeField] private WorldTile m_BlightCore = null;

    protected override void OnGenerateBaseTile(int x, int y)
    {
        if (x == m_Width / 2 && y == m_Height / 2)
        {
            m_Map.PlaceTileAt(m_BlightCore,x,y);
        }
        
        base.OnGenerateBaseTile(x, y);
    }
}