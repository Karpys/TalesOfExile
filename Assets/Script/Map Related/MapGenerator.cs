using UnityEngine;
using Random = UnityEngine.Random;

[System.Serializable]
public class BoardEntitySpawn
{
    public BoardEntity Entity = null;
    public Vector2Int BoardSpawnPosition = Vector2Int.zero;
}
public class MapGenerator : MonoBehaviour
{
    [SerializeField] private MapData m_MapData = null;
    [SerializeField] private BoardEntitySpawn[] m_BoardEntities = null;
    [SerializeField] private MapGenerationData m_GenerationData = null;

    private void Start()
    {
        InitializeMap();
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            m_GenerationData.Generate(m_MapData);
        }
    }

    private void InitializeMap()
    {
        Map map = new Map();
        m_MapData.Map = map;
        InitializeMapData();
        //Tiles Initiation//
        
        GenerationMapInfo info = m_GenerationData.Generate(m_MapData);
        
        //Entities Init//
        foreach (BoardEntitySpawn boardEntity in m_BoardEntities)
        {
            BoardEntity entity = Instantiate(boardEntity.Entity, transform.position, Quaternion.identity, m_MapData.transform);

            if (entity as PlayerBoardEntity)
            {
                entity.Place(info.StartPosition.x,info.StartPosition.y,m_MapData);
                continue;
            }
            
            entity.Place(boardEntity.BoardSpawnPosition.x,boardEntity.BoardSpawnPosition.y,m_MapData);
        }
    }

    

    private void InitializeMapData()
    {
        PathFinding.mapData = m_MapData;
        LinePath.mapData = m_MapData;
    }
}