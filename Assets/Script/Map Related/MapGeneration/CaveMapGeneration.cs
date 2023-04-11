using UnityEngine;

[CreateAssetMenu(menuName = "Map/Room/Cave", fileName = "New CaveMap", order = 0)]
public class CaveMapGeneration : MapGenerationData
{
    [SerializeField] protected WorldTile m_HoleTile = null;
    [SerializeField] private Vector2Int m_StartPosition = Vector2Int.zero;
    [SerializeField] private Zone m_StartZone = null;

    public override GenerationMapInfo Generate(MapData mapData)
    {
        GenerationMapInfo info = base.Generate(mapData);
        info.StartPosition = m_StartPosition;

        MapHelper.ZoneTryPlaceTile(m_HoleTile,m_StartZone,m_StartPosition,m_Map);

        Room testRoom = new OuterSquareDigRoom(new Map(25, 25), m_HoleTile, new Zone(ZoneType.Circle, 9),
            new Zone(ZoneType.OuterCircle, 9), new Zone(ZoneType.Circle, 4), 20);
        
        testRoom.Generate();
        
        foreach (Tile mapTile in testRoom.Map.Tiles)
        {
            if(!mapTile.WorldTile)
                continue;

            m_Map.InsertWorldTileAt(mapTile.WorldTile, mapTile.TilePosition + new Vector2Int(25/2,m_StartPosition.y + (m_StartZone.Range)));
        }
        
        return info;
    }
}