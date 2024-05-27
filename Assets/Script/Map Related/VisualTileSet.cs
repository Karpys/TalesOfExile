namespace KarpysDev.Script.Map_Related
{
    using UnityEngine;
    using Widget;

    public class VisualTileSet : VisualTile,IVisualTile
    {
        [SerializeField] private TileSetType m_TileSetType = TileSetType.DirtRoad;

        private Vector2Int m_Position = Vector2Int.zero;
        public Vector2Int Position => m_Position;

        public override void Place(Vector2Int position)
        {
            base.Place(position);
            m_Position = position;
            TileSetManager.Instance.AddVisualTile(this,m_TileSetType);
        }

        public void ApplySprite(Sprite sprite)
        {
            m_Renderer.sprite = sprite;
        }
    }
}