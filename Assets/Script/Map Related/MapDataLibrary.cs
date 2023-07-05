using KarpysDev.Script.Map_Related.MapGeneration;
using UnityEngine;

namespace KarpysDev.Script.Map_Related
{
    public class MapDataLibrary : SingletonMonoBehavior<MapDataLibrary>
    {
        public float TileSize = 0;
        [SerializeField] private MapTileReloader BaseMapReloader = null;
        [SerializeField] private MapTileReloader PortalMapReloader = null;

        public void AddReloaderAt(Vector2Int pos)
        {
            Instantiate(BaseMapReloader, MapData.Instance.transform).SetReloadPosition(pos);
        }

        public void AddPortalMapReloaderAt(Vector2Int pos)
        {
            Instantiate(PortalMapReloader, MapData.Instance.transform).SetReloadPosition(pos);
        }
    }
}