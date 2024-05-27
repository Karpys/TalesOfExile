using System;
using System.Collections.Generic;
using KarpysDev.KarpysUtils;
using KarpysDev.KarpysUtils.ObjectPooling;
using KarpysDev.Script.Manager;
using KarpysDev.Script.Widget.DebugMenu;
using UnityEngine;
using UnityEngine.UI;

namespace KarpysDev.Script.Map_Related.Minimap
{
    public class MinimapRenderer : SingletonMonoBehavior<MinimapRenderer>,IReturnable<DynamicMinimapTile>
    {
        [SerializeField] private Image m_RenderImage = null;
        [SerializeField] private RectTransform m_PivotTransform = null;
        [SerializeField] private Transform m_DynamicTileParent = null;
        [SerializeField] private int m_initialSize = 30;
        [SerializeField] private DynamicMinimapTile m_DynamicTile = null;
        [SerializeField] private Color m_DefaultColor = Color.black;
        [SerializeField] private float m_ScaleFactor = 1;

        private IPositionable m_Pivot = null;
        private GameObjectPool<DynamicMinimapTile> m_DynamicPool = null;
        private LinkedList<DynamicMinimapTile> m_DynamicMinimapTiles = new LinkedList<DynamicMinimapTile>();
        private void Awake()
        {
            m_DynamicPool = new GameObjectPool<DynamicMinimapTile>(m_DynamicTile, m_DynamicTileParent, m_initialSize, OnAddNewDynamicTile);
            DebugMenu.Instance.AddDebugButton(BaseMinimapRender,"Render Minimap");
            GameManager.Instance.A_OnEndTurn += UpdateDynamicTiles;
            GameManager.Instance.A_OnEndTurn += RecenterAroundPivot;
        }

        #if UNITY_EDITOR
        private float m_PreviousScaleFactor = 0;
        private void Update()
        {
            if (Math.Abs(m_PreviousScaleFactor - m_ScaleFactor) > 0.005f)
            {
                m_PreviousScaleFactor = m_ScaleFactor;
                Rebuild();

                int childCount = m_DynamicTileParent.childCount;

                for (int i = 0; i < childCount; i++)
                {
                    Transform t = m_DynamicTileParent.GetChild(i);
                    if (t is RectTransform rectTransform)
                    {
                        rectTransform.sizeDelta = new Vector2(m_ScaleFactor, m_ScaleFactor);
                    }
                }
                
                UpdateDynamicTiles();
            }
        }
        #endif

        private void RecenterAroundPivot()
        {
            if(m_Pivot == null)
                return;

            Vector2 newPosition = -m_Pivot.Position * m_ScaleFactor - new Vector2(m_ScaleFactor,m_ScaleFactor)/2;
            m_PivotTransform.anchoredPosition = newPosition;
        }

        public void Rebuild()
        {
            BaseMinimapRender();
            RecenterAroundPivot();
            UpdateDynamicTiles();
        }

        private void OnAddNewDynamicTile(DynamicMinimapTile newTile)
        {
            newTile.rectTransform.sizeDelta = new Vector2(m_ScaleFactor, m_ScaleFactor);
            newTile.Initialize(this);
        }
        
        private void BaseMinimapRender()
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

        public DynamicMinimapTile AddDynamicTile(IPositionable positionable,Vector2Int position, Color tileColor)
        {
            DynamicMinimapTile tile = m_DynamicPool.Take();
            tile.rectTransform.anchoredPosition = new Vector2(position.x * m_ScaleFactor, position.y * m_ScaleFactor);
            tile.color = tileColor;
            tile.AssignPositionable(positionable);
            m_DynamicMinimapTiles.AddLast(tile);
            return tile;
        }
        
        public void SetPivot(IPositionable positionable)
        {
            m_Pivot = positionable;
        }

        public void Return(DynamicMinimapTile obj)
        {
            obj.Clear();
            m_DynamicPool.Return(obj);
            m_DynamicMinimapTiles.Remove(obj);
        }

        private void UpdateDynamicTiles()
        {
            foreach (DynamicMinimapTile dynamicMinimapTile in m_DynamicMinimapTiles)
            {
                Vector2 position = dynamicMinimapTile.GetPosition();
                dynamicMinimapTile.rectTransform.anchoredPosition = new Vector2(position.x * m_ScaleFactor, position.y * m_ScaleFactor);
            }
        }
    }

    public interface IPositionable
    {
        public Vector2 Position{ get;}
    }
}