using UnityEngine;

namespace KarpysDev.Script.Map_Related
{
    public class MapPlaceable : MonoBehaviour
    {
        protected Vector2Int m_Position = Vector2Int.zero;

        public virtual void Place(Vector2Int position)
        {
            transform.position = MapData.Instance.GetTilePosition(position);
            m_Position = position;
        }
    }
}