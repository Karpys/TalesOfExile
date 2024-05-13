using KarpysDev.KarpysUtils;
using KarpysDev.KarpysUtils.ObjectPooling;
using KarpysDev.Script.Widget.DebugMenu;
using UnityEngine;
using UnityEngine.UI;

namespace KarpysDev.Script.Map_Related.Minimap
{
    public class MinimapRenderer : SingletonMonoBehavior<MinimapRenderer>,IReturnable<DynamicMinimapTile>
    {
        [SerializeField] private Image m_RenderImage = null;
        [SerializeField] private Transform m_DynamicTileParent = null;
        [SerializeField] private int m_initialSize = 30;
        [SerializeField] private DynamicMinimapTile m_DynamicTile = null;
        [SerializeField] private Color m_DefaultColor = Color.black;
        [SerializeField] private float m_ScaleFactor = 1;

        private GameObjectPool<DynamicMinimapTile> m_DynamicPool = null;
        private void Awake()
        {
            m_DynamicPool = new GameObjectPool<DynamicMinimapTile>(m_DynamicTile, m_DynamicTileParent, m_initialSize, OnAddNewDynamicTile);
            DebugMenu.Instance.AddDebugButton(InitialRender,"Render Minimap");
        }
        
        private void OnAddNewDynamicTile(DynamicMinimapTile newTile)
        {
            newTile.rectTransform.sizeDelta = new Vector2(m_ScaleFactor, m_ScaleFactor);
            newTile.Initialize(this);
        }
        
        private void InitialRender()
        {
            Map map = MapData.Instance.Map;
            Texture2D minimapTexture = new Texture2D(map.Width, map.Height);
            
            for (int x = 0; x < map.Width; x++)
            {
                for (int y = 0; y < map.Height; y++)
                {
                    minimapTexture.SetPixel(x, y, map.Tiles[x][y].WorldTile ? map.Tiles[x][y].WorldTile.MinimapColor : m_DefaultColor);
                }
            }

            minimapTexture.filterMode = FilterMode.Point;
            minimapTexture.Apply();

            Sprite sprite = Sprite.Create(minimapTexture,new Rect(Vector2.zero, new Vector2(map.Width,map.Height)),Vector2.one * .5f);
            m_RenderImage.rectTransform.sizeDelta = new Vector2(map.Width * m_ScaleFactor, map.Height * m_ScaleFactor);
            m_RenderImage.sprite = sprite;
        }

        public DynamicMinimapTile AddDynamicTile(Vector2Int position, Color tileColor)
        {
            DynamicMinimapTile tile = m_DynamicPool.Take();
            tile.rectTransform.anchoredPosition = new Vector2(position.x * m_ScaleFactor, position.y * m_ScaleFactor);
            tile.color = tileColor;
            return tile;
        }

        public void Return(DynamicMinimapTile obj)
        {
            m_DynamicPool.Return(obj);
        }
    }
}