using UnityEngine;

public class MapDataLibrary : SingletonMonoBehavior<MapDataLibrary>
{
    public float TileSize = 0;
    [SerializeField] private MapTileReloader BaseMapReloader = null;

    public void AddReloaderAt(Vector2Int pos)
    {
        Instantiate(BaseMapReloader, MapData.Instance.transform).SetReloadPosition(pos);
    }
}