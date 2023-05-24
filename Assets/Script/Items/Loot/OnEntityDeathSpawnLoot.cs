using System.Collections.Generic;
using System.Linq;
using KarpysDev.Script.Entities;
using KarpysDev.Script.Map_Related;
using KarpysDev.Script.Utils;
using UnityEngine;

namespace KarpysDev.Script.Items.Loot
{
    public class OnEntityDeathSpawnLoot : Lootable
    {
        [SerializeField] private InventoryItemData[] m_EntityLoot = null;
        private BoardEntity m_AttachedEntity = null;

        private void Awake()
        {
            m_AttachedEntity = GetComponent<BoardEntity>();
        }

        private void Start()
        {
            m_AttachedEntity.EntityEvent.OnDeath += SpawnLoot;
            ComputeLoot();
        }

        private void OnDestroy()
        {
            m_AttachedEntity.EntityEvent.OnDeath -= SpawnLoot;
        }

        protected override void ComputeLoot()
        {
            base.ComputeLoot();
            m_LootObjects = m_EntityLoot.Select(loot => LootUtils.ToInventoryObject(loot)).ToList();
        }

        protected override Vector2Int GetOriginPosition()
        {
            return m_AttachedEntity.EntityPosition;
        }

        protected override Tile GetOriginTile()
        {
            return MapData.Instance.GetTile(m_AttachedEntity.EntityPosition);
        }

        protected override List<Tile> GetLootTiles()
        {
            MapData mapData = MapData.Instance;
            Vector2Int entityPosition = m_AttachedEntity.EntityPosition;
        
            foreach (Vector2Int position in m_LootZones)
            {
                Tile tile = mapData.GetTile(position + entityPosition);

                if (tile is {Walkable: true})
                {
                    m_LootTiles.Add(tile);
                }
            }

            return m_LootTiles;
        }
    }
}