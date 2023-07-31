using System;
using System.Collections.Generic;
using KarpysDev.Script.Entities;
using KarpysDev.Script.Entities.EntitiesBehaviour;
using KarpysDev.Script.Manager;
using KarpysDev.Script.Map_Related.MapGeneration;
using KarpysDev.Script.Map_Related.QuestRelated;
using KarpysDev.Script.UI;
using KarpysDev.Script.Widget;
using UnityEngine;

namespace KarpysDev.Script.Map_Related
{
    public class MapGenerator : SingletonMonoBehavior<MapGenerator>
    {
        [SerializeField] private MapData m_MapData = null;
        [SerializeField] private PlayerBoardEntity m_PlayerEntity = null;
        [SerializeField] private MapGenerationData m_HubMap = null;
        [SerializeField] private QuestModifierManager m_QuestModifier = null;

        private bool m_FirstGeneration = true;
        private int m_MapId = 0;
        private Quest m_CurrentQuest = null;
        public MapGenerationData CurrentMapData => m_CurrentQuest.MapGroup.MapGenerationData[m_MapId];

        public Action A_OnMapErased = null;
        public Action A_OnMapLoaded = null;
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
            LoadMap(m_HubMap);
        }

        public void ReloadMap()
        {
            LoadMap(CurrentMapData);
        }

        private void ReturnToHub()
        {
            m_QuestModifier.Clear();
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
            
            A_OnMapErased?.Invoke();
        }

        public void NextMap()
        {
            m_MapId += 1;

            if (m_MapId >= m_CurrentQuest.MapGroup.MapGenerationData.Length)
            {
                m_MapId = 0;
                ReturnToHub();
                MissionSelectionManager.Instance.TriggerQuestEnd();
                return;
            }
        
            LoadMap(m_CurrentQuest.MapGroup.MapGenerationData[m_MapId]);
        }

        private void LoadMap(MapGenerationData generationData)
        {
            MapCleaner.Instance.Clean();
            EraseMap();
            GenerationMapInfo info = generationData.Generate(m_MapData);
            PlacePlayerEntity(info.StartPosition);
            A_OnMapLoaded?.Invoke();
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

        public void SetQuest(Quest quest)
        {
            m_CurrentQuest = quest;
            m_MapId = 0;
        }

        public void LaunchQuest()
        {
            m_QuestModifier.ApplyQuestModifier(m_CurrentQuest);
            LoadMap(CurrentMapData);
        }

        private void InitializeMapData()
        {
            PathFinding.PathFinding.mapData = m_MapData;
        }
    }
}