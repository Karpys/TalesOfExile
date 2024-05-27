namespace KarpysDev.Script.Map_Related
{
    using UnityEngine;

    public interface IVisualTile
    {
        public Vector2Int Position { get;}
        public void ApplySprite(Sprite sprite);
    }
}