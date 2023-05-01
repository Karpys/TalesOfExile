using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Map/SpriteMap", fileName = "SpriteMap", order = 0)]
public class SpriteMapGeneration : MapGenerationData
{
    [SerializeField] private Sprite m_MapSprite = null;
    [SerializeField] private GenericLibrary<WorldTile, Color> m_ColorTileMap = null;

    public Sprite MapSprite => m_MapSprite;

    public void GenerateLibrary(List<Color> colors)
    {
        LibraryKey<WorldTile, Color>[] keys = new LibraryKey<WorldTile, Color>[colors.Count];

        for (int i = 0; i < colors.Count; i++)
        {
            keys[i] = new LibraryKey<WorldTile, Color>(null, colors[i]);
        }
        
        m_ColorTileMap.SetKeys(keys);
    }
}