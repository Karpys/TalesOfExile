using UnityEngine;

//Used to get info from the created map
namespace KarpysDev.Script.Map_Related
{
    using System.Collections.Generic;

    public class GenerationMapInfo
    {
        public Vector2Int StartPosition = Vector2Int.zero;

        public GenerationMapInfo(Vector2Int startPosition)
        {
            StartPosition = startPosition;
        }

        public GenerationMapInfo(){}
    }
    
    public class GenerationMapInfoEditor
    {
        public Vector2Int StartPosition = Vector2Int.zero;
        public List<WorldTile> WorldTiles = new List<WorldTile>();

        public GenerationMapInfoEditor(Vector2Int startPosition,List<WorldTile> tiles)
        {
            StartPosition = startPosition;
            WorldTiles = tiles;
        }

        public GenerationMapInfoEditor(){}
    }
}
