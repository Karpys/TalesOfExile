using UnityEngine;

namespace KarpysDev.Script.Entities
{
    public class WalkableEntity : BoardEntity
    {
        protected override void OnNewPosition(Vector2Int position)
        {
            return;
        }

        protected override void RemoveFromBoard()
        {
            return;
        }
    }
}