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
        Map map = new Map();
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
            foreach (BoardEntitySpawn boardEntity in m_BoardEntities)
            {
                BoardEntity entity = Instantiate(boardEntity.Entity, transform.position, Quaternion.identity, m_MapData.transform);

                if (entity as PlayerBoardEntity)
                {
                    entity.Place(info.StartPosition.x,info.StartPosition.y,m_MapData);
                    continue;
                }
            }
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
        LinePath.mapData = m_MapData;
    }
}