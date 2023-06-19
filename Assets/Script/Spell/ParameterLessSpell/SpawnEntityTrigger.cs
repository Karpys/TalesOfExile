using KarpysDev.Script.Entities;
using KarpysDev.Script.Entities.EntitiesBehaviour;
using KarpysDev.Script.Manager.Library;
using KarpysDev.Script.Map_Related;
using KarpysDev.Script.Widget;
using UnityEngine;

namespace KarpysDev.Script.Spell.ParameterLessSpell
{
    public class SpawnEntityTrigger : SelectionSpellTrigger
    {
        private EntityType m_EntityTypeToSpawn = EntityType.None;
        private bool m_UseTransmitter = false;
        public SpawnEntityTrigger(BaseSpellTriggerScriptable baseScriptable,EntityType entityType,bool useTransmitter) : base(baseScriptable)
        {
            m_EntityTypeToSpawn = entityType;
            m_UseTransmitter = useTransmitter;
        }

        protected override void TileHit(Vector2Int tilePosition, TriggerSpellData spellData)
        {
            base.TileHit(tilePosition, spellData);

            Tile tile = MapData.Instance.GetTile(tilePosition);

            Debug.Log(tilePosition);
            if (tile.Walkable)
            {
                BoardEntity entity = EntityHelper.SpawnEntityOnMap(tilePosition,EntityLibrary.Instance.GetEntityViaKey(m_EntityTypeToSpawn)
                    ,GetEntityIa(),spellData.AttachedEntity.EntityGroup,spellData.AttachedEntity.TargetEntityGroup);
            
                if(m_UseTransmitter)
                    entity.GetComponent<StatsTransmitter>().InitTransmitter(spellData.AttachedEntity);
            }
        }

        protected virtual BaseEntityIA GetEntityIa()
        {
            return new BaseEntityIA();
        }
    }

    public class ReplaceSpawnTrigger : SpawnEntityTrigger
    {
        public ReplaceSpawnTrigger(BaseSpellTriggerScriptable baseScriptable, EntityType entityType, bool useTransmitter) : base(baseScriptable, entityType, useTransmitter)
        {
        }

        protected override void Trigger(TriggerSpellData spellData, SpellTiles spellTiles, CastInfo castInfo, float efficiency = 1)
        {
            spellData.AttachedEntity.ForceDeath();
            base.Trigger(spellData, spellTiles, castInfo,efficiency);
        }
    }
}