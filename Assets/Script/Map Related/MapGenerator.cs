using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

[System.Serializable]
public class BoardEntitySpawn
{
    public BoardEntity Entity = null;
    public Vector2Int BoardSpawnPosition = Vector2Int.zero;
}
public class MapGenerator : MonoBehaviour
{
    [SerializeField] private int m_Width = 0;
    [SerializeField] private int m_Height = 0;
    [SerializeField] private float m_TileSize = 0;
    [SerializeField] private Tile[] m_GrassGround = null;
    [SerializeField] private SpriteHelper m_TreeVisual = null;
    [SerializeField] private MapData m_MapData = null;
    [SerializeField] private BoardEntitySpawn[] m_BoardEntities = null;

    private void Start()
    {
        InitializeMap();
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            EraseMap();
            InitializeMap();
        }
    }

    private void EraseMap()
    {
        if(m_MapData.Map.Tiles == null) return;
        
        //Erase Tiles//
        for (int x = 0; x < m_MapData.Map.Width; x++)
        {
            for (int y = 0; y < m_MapData.Map.Height; y++)
            {
                Destroy(m_MapData.Map.Tiles[x,y]);
            }
        }
        m_MapData.Map.Tiles = null;
        
        //Erase Entities//
        for (int i = 0; i < m_MapData.Map.EntitiesOnBoard.Count; i++)
        {
            Destroy(m_MapData.Map.EntitiesOnBoard[i]);
        }
        m_MapData.Map.EntitiesOnBoard.Clear();
    }

    private void InitializeMap()
    {
        Map map = new Map();
        m_MapData.Map = map;
        //Tiles Initiation//
        map.Height = m_Height;
        map.Width = m_Width;
        map.Tiles = new Tile[m_Width,m_Height];
        
        for (int x = 0; x < m_Width; x++)
        {
            for (int y = 0; y < m_Height; y++)
            {
                Tile tile = Instantiate(m_GrassGround[Random.Range(0,m_GrassGround.Length)], m_MapData.GetTilePosition(x,y),Quaternion.identity,m_MapData.transform);
                tile.Initialize(x,y);
                map.Tiles[x,y] = tile;
                
                if (x == 0 || y == 0 || x == m_Height - 1 || y == m_Width - 1)
                {
                    SpriteHelper spr = Instantiate(m_TreeVisual, m_MapData.GetTilePosition(x,y),Quaternion.identity,m_MapData.transform);
                    spr.SetSpritePriority(-y);
                }
            }
        }

        //Entities Init//
        PlayerBoardEntity player = null;
        foreach (BoardEntitySpawn boardEntity in m_BoardEntities)
        {
            BoardEntity entity = Instantiate(boardEntity.Entity, transform.position, Quaternion.identity, m_MapData.transform);
            entity.Place(boardEntity.BoardSpawnPosition.x,boardEntity.BoardSpawnPosition.y,m_MapData);
            m_MapData.Map.EntitiesOnBoard.Add(entity);
            
            PlayerBoardEntity playerEntity = entity as PlayerBoardEntity;
            if (playerEntity)
            {
                player = playerEntity;
            }
        }

        m_MapData.InitializeMapData(player);
    }
}