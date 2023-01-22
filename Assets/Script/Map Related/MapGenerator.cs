using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class MapGenerator : MonoBehaviour
{
    [SerializeField] private int m_Width = 0;
    [SerializeField] private int m_Height = 0;
    [SerializeField] private float m_TileSize = 0;
    [SerializeField] private Tile m_PrefabTile = null;
    [SerializeField] private MapData m_MapData = null;

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
        
        for (int x = 0; x < m_MapData.Map.Width; x++)
        {
            for (int y = 0; y < m_MapData.Map.Height; y++)
            {
                Destroy(m_MapData.Map.Tiles[x][y]);
            }
        }
        
        m_MapData.Map.Tiles = null;
    }

    private void InitializeMap()
    {
        Map map = new Map();
        map.Height = m_Height;
        map.Width = m_Width;
        map.Tiles = new Tile[m_Width][];

        for (int x = 0; x < m_Width; x++)
        {
            map.Tiles[x] = new Tile[m_Height];
            for (int y = 0; y < m_Height; y++)
            {
                Tile tile = Instantiate(m_PrefabTile, GetTilePosition(x, y, m_TileSize),Quaternion.identity,m_MapData.transform);
                tile.Initialize(x,y);
                map.Tiles[x][y] = tile;
            }
        }
    }

    private Vector3 GetTilePosition(int x, int y, float tileSize)
    {
        return new Vector3(x * tileSize, y * tileSize, 0);
    }
}

public class MapDataLibrary : MonoBehaviour
{
    
}

public class BoardEntity : MonoBehaviour
{
    [SerializeField] private int m_XPosition = 0;
    [SerializeField] private int m_YPosition = 0;

    private MapData m_TargetMap = null;

    public void Place(int x, int y, MapData targetMap)
    {
        m_XPosition = x;
        m_YPosition = y;
        m_TargetMap = targetMap;
    }
}