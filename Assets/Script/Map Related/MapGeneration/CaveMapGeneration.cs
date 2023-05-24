using KarpysDev.Script.Spell;
using KarpysDev.Script.UI.UnityWorldDebug;
using KarpysDev.Script.Utils;
using UnityEngine;

namespace KarpysDev.Script.Map_Related.MapGeneration
{
    [CreateAssetMenu(menuName = "Map/Room/Cave", fileName = "New CaveMap", order = 0)]
    public class CaveMapGeneration : FlatDefaultMapGeneration
    {
        [Header("World Tile")]
        [SerializeField] protected WorldTile m_DoorTile = null;
        [SerializeField] protected WorldTile m_HoleTile = null;
        [Header("Generation Parameters")]
        [SerializeField] private Vector2Int m_StartPosition = Vector2Int.zero;
        [SerializeField] private Zone m_StartZone = null;

        [SerializeField] private Vector2Int m_RoomDimension = Vector2Int.zero;
        public override GenerationMapInfo Generate(MapData mapData)
        {
            GenerationMapInfo info = base.Generate(mapData);
            info.StartPosition = m_StartPosition;
        
            SpawnPositionGeneration();
        
            InsertOuterSquareRoom(new Vector2Int(0,0),m_RoomDimension,70);

            return info;
        }

        private void SpawnPositionGeneration()
        {
            MapHelper.ZoneTryPlaceTile(m_HoleTile,m_StartZone,m_StartPosition,m_Map);
            m_Map.TryPlaceTileAt(m_DoorTile, m_StartPosition + new Vector2Int(0,m_StartZone.Range));
        }

        private void InsertOuterSquareRoom(Vector2Int offSet,Vector2Int roomDimension,float fillPercentage,float shrinkChance = 20f)
        {
            Vector2Int originPosition = new Vector2Int(0, m_StartPosition.y + m_StartZone.Range) + offSet;
            Room room = new OuterSquareDigRoom(new Map(roomDimension.x,roomDimension.y), m_HoleTile, fillPercentage, ZoneType.Circle, shrinkChance);
            room.Generate();
            GizmosViewerManager.Instance.CreateCube(MapData.Instance.GetTilePosition(originPosition + (roomDimension/2)),new Vector3(roomDimension.x,roomDimension.y),Color.white);
        
            MapHelper.InsertMapInsideMap(m_Map,room.Map,originPosition);
        }
    }
}