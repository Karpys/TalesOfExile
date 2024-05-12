using KarpysDev.Script.Widget.DebugMenu;
using UnityEngine;

namespace KarpysDev.Script.Map_Related.Minimap
{
    public class MinimapRenderer : MonoBehaviour
    {
        [SerializeField] private Color m_DefaultColor = Color.black;
        private void Awake()
        {
            DebugMenu.Instance.AddDebugButton(Render,"Render Minimap");
        }
        public void Render()
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

            Debug.Log("Render");
        }
    }
}