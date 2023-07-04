using System.Collections.Generic;
using KarpysDev.Script.Widget;
using UnityEngine;

namespace KarpysDev.Script.Map_Related.MapGeneration
{
    [CreateAssetMenu(menuName = "Map/SpriteMap", fileName = "SpriteMap", order = 0)]
    public class SpriteMapGeneration : MapGenerationData
    {
        [Header("Sprite Map Data")]
        [SerializeField] private Sprite m_MapSprite = null;
        [SerializeField] private GenericLibrary<WorldTile, Color> m_ColorTileMap = null;

        [SerializeField] private BaseMonsterGeneration m_MonsterGeneration = null;
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

        private void InitColorLibrary()
        {
            m_ColorTileMap.InitializeDictionary();
        }

        public override GenerationMapInfo Generate(MapData mapData)
        {
            InitColorLibrary();
        
            m_Width = m_MapSprite.texture.width;
            m_Height = m_MapSprite.texture.height;
            base.Generate(mapData);
            
            GenerateTiles();
            
            MonsterGeneration();
            
            return new GenerationMapInfo(m_SpawnPosition);
        }

        protected virtual void GenerateTiles()
        {
            Texture2D tex = m_MapSprite.texture;
        
            for (int x = 0; x < m_Width; x++)
            {
                for (int y = 0; y < m_Height; y++)
                {
                    m_Map.Tiles[x, y] = new Tile(x,y);
                    Color tileColor = tex.GetPixel(x, y);
                    WorldTile tile = null;

                    //Strange fix, the try get value seems to not working with color, even with advend debuging//
                    //Dont know but this trick work, fine :/ //
                    foreach (KeyValuePair<Color,WorldTile> keyValuePair in m_ColorTileMap.Dictionary)
                    {
                        if (tileColor.rgb() == keyValuePair.Key.rgb())
                        {
                            tile = keyValuePair.Value;
                            break;
                        }
                    }

                
                    if(tile)
                        m_Map.PlaceTileAt(tile, x, y);
                }
            }
        }

        private void MonsterGeneration()
        {
            List<Tile> tiles = GetMonsterTiles();
            
            if(m_MonsterGeneration)
                m_MonsterGeneration.Generate(tiles);
        }

        protected virtual List<Tile> GetMonsterTiles()
        {
            List<Tile> tiles = new List<Tile>();
            
            foreach (Tile tile in m_MapData.Map.Tiles)
            {
                if(tile.Walkable)
                    tiles.Add(tile);
            }

            return tiles;
        }
    }
}