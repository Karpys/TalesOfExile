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
    [SerializeField] private BoardEntity m_Character = null;

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
                Tile tile = Instantiate(m_PrefabTile, m_MapData.GetTilePosition(x,y),Quaternion.identity,m_MapData.transform);
                tile.Initialize(x,y);
                map.Tiles[x][y] = tile;
            }
        }

        m_Character.Place(5, 5, m_MapData);
    }
}