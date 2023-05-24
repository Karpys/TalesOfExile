using System.Collections.Generic;
using KarpysDev.Script.Entities;
using KarpysDev.Script.Entities.EntitiesBehaviour;
using KarpysDev.Script.Manager;
using KarpysDev.Script.Map_Related.MapGeneration;
using KarpysDev.Script.Widget;
using UnityEngine;

namespace KarpysDev.Script.Map_Related
{
    public class MapGenerator : SingletonMonoBehavior<MapGenerator>
    {
        [SerializeField] private MapData m_MapData = null;
        [SerializeField] private PlayerBoardEntity m_PlayerEntity = null;
        [SerializeField] private MapGenerationData m_HubMap = null;
        [SerializeField] private MapGroup m_CurrentMapGroup = null;

        private bool m_FirstGeneration = true;
        private int m_MapId = 0;
        public MapGenerationData CurrentMapData => m_CurrentMapGroup.MapGenerationData[m_MapId];
        private void Start()
        {
            InitializeMap();
        }

        public void Update()
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                ReloadMap();
            }else if (Input.GetKeyDown(KeyCode.N))
            {
                NextMap();
            }
        }
    
        private void InitializeMap()
        {
            InitializeMapData();
            //Tiles Initiation//
            LoadMap(CurrentMapData);
        }

        private void ReloadMap()
        {
            LoadMap(CurrentMapData);
        }

        private void ReturnToHub()
        {
            LoadMap(m_HubMap);
        }
    
        private void EraseMap()
        {
            if(m_MapData.Map == null)
                return;
        
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

        public void NextMap()
        {
            m_MapId += 1;

            if (m_MapId >= m_CurrentMapGroup.MapGenerationData.Length)
            {
                m_MapId = 0;
                ReturnToHub();
                return;
            }
        
            LoadMap(m_CurrentMapGroup.MapGenerationData[m_MapId]);
        }

        private void LoadMap(MapGenerationData generationData)
        {
            MapCleaner.Instance.Clean();
            EraseMap();
            GenerationMapInfo info = generationData.Generate(m_MapData);
        
            PlacePlayerEntity(info.StartPosition);
        }

        private void PlacePlayerEntity(Vector2Int position)
        {
            if (m_FirstGeneration)
            {
                EntityHelper.SpawnEntityOnMap(position,m_PlayerEntity,new PLayerAutoPlayEntity(),EntityGroup.Friendly);
            }
            else
            {
                GameManager.Instance.PlayerEntity.Place(position.x,position.y,m_MapData);   
            }

            m_FirstGeneration = false;
        }

        public void SetMapGroup(MapGroup mapGroup)
        {
            m_CurrentMapGroup = mapGroup;
            m_MapId = 0;
        }

        private void InitializeMapData()
        {
            PathFinding.PathFinding.mapData = m_MapData;
        }
    }
}