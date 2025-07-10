using KarpysDev.Script.Map_Related.MapGeneration;
using KarpysDev.Script.Map_Related.QuestRelated;
using UnityEngine;

namespace KarpysDev.Script.Map_Related
{
    using KarpysUtils;

    public class MapDataLibrary : SingletonMonoBehavior<MapDataLibrary>
    {
        [SerializeField] private MapTileReloader m_BaseMapReloader = null;
        [SerializeField] private MapTileReloader m_PortalMapReloader = null;
        [SerializeField] private MissionLauncherPortalMap m_MissionLauncherPortalMap = null;

        public const float TILE_SIZE = 1;
        public void AddReloaderAt(Vector2Int pos)
        {
            Instantiate(m_BaseMapReloader, MapData.Instance.TileHolder).Initialize(pos);
        }

        public void AddPortalMapReloaderAt(Vector2Int pos)
        {
            Instantiate(m_PortalMapReloader, MapData.Instance.TileHolder).Initialize(pos);
        }
        
        public void AddMissionLauncher(Vector2Int pos,Sprite sprite,Quest quest)
        {
            MissionLauncherPortalMap missionPortal = Instantiate(m_MissionLauncherPortalMap, MapData.Instance.TileHolder);
            missionPortal.Initialize(pos);
            missionPortal.Init(sprite,quest);
        }
    }
}