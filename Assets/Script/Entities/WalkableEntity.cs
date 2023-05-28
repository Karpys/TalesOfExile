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

        public override void MoveTo(int x, int y, bool movement = true)
        {
            m_XPosition = x;
            m_YPosition = y;
            OnNewPosition(EntityPosition);
            //OnMove ?//
            if(movement)
                Movement();
        }
    }
}