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

        InsertOuterSquareRoom(new Vector2Int(0,0));
        InsertOuterSquareRoom(new Vector2Int(25,0));
        InsertOuterSquareRoom(new Vector2Int(0,25));
        InsertOuterSquareRoom(new Vector2Int(25,25));
        
        return info;
    }

    private void InsertOuterSquareRoom(Vector2Int offSet)
    {
        Room outerSquareDigRoom = new OuterSquareDigRoom(new Map(25, 25), m_HoleTile, new Zone(ZoneType.Circle, 9),
            new Zone(ZoneType.OuterCircle, 9), new Zone(ZoneType.Circle, 4), 20);
        outerSquareDigRoom.Generate();
        
        MapHelper.InsertMapInsideMap(m_Map,outerSquareDigRoom.Map,new Vector2Int(0, m_StartPosition.y + m_StartZone.Range) + offSet);
    }
}