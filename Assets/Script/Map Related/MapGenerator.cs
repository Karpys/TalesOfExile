using System.Collections.Generic;
using System.Linq;
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

    private bool m_FirstGeneration = true;
    public MapGenerationData GenerationData => m_GenerationData;
    private void Start()
    {
        InitializeMap();
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            EraseMap();
            GenerateMap();
        }
    }

    private void EraseMap()
    {
        List<BoardEntity> entity = GameManager.Instance.EntitiesOnBoard;
        
        int entityCount = entity.Count;
        int min = 0;

        for (int i = 0; i < entityCount; i++)
        {
            if (entity[min] as PlayerBoardEntity)
            {
                min += 1;
                continue;
            }
            
            entity[min].ForceDeath();
        }

        foreach (Tile tile in m_MapData.Map.Tiles)
        {
            Destroy(tile.WorldTile.gameObject);
        }
    }

    private void InitializeMap()
    {
        Map map = new Map(this);
        m_MapData.Map = map;
        InitializeMapData();
        //Tiles Initiation//
        GenerateMap();

    }

    private void GenerateMap()
    {
        GenerationMapInfo info = m_GenerationData.Generate(m_MapData);
        
        //Entities Init//
        if (m_FirstGeneration)
        {
            EntityHelper.SpawnEntityOnMap(m_BoardEntities[0].Entity, info.StartPosition.x, info.StartPosition.y, m_MapData);
        }
        else
        {
            Debug.Log("Move Player");
            GameManager.Instance.PlayerEntity.MoveTo(info.StartPosition,false);   
        }

        m_FirstGeneration = false;
    }

    

    private void InitializeMapData()
    {
        PathFinding.mapData = m_MapData;
    }
}