using UnityEngine;

//Used to get info from the created map
namespace KarpysDev.Script.Map_Related
{
    public class GenerationMapInfo
    {
        public Vector2Int StartPosition = Vector2Int.zero;

        public GenerationMapInfo(Vector2Int startPosition)
        {
            StartPosition = startPosition;
        }

        public GenerationMapInfo(){}
    }
}
