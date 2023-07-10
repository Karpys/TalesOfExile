using System.Collections.Generic;
using KarpysDev.Script.Entities;
using KarpysDev.Script.Manager.Library;
using KarpysDev.Script.Map_Related;
using UnityEngine;

namespace KarpysDev.Script.Spell
{
    class CreateStormAreaPlaceableTrigger : CreateSpellPlaceableTrigger
    {
        public CreateStormAreaPlaceableTrigger(BaseSpellTriggerScriptable baseScriptable, PlaceableType placeableType, BehaveTiming behaveTiming, int behaveCount) : base(baseScriptable, placeableType, behaveTiming, behaveCount)
        {}

        private Vector2Int m_StormOrigin = Vector2Int.zero;
        private List<Vector2Int> m_StormTilesArea = new List<Vector2Int>();
        private LightningStormPlaceable m_StormArea = null;

        protected override void Trigger(TriggerSpellData spellData, SpellTiles spellTiles, CastInfo castInfo, float efficiency = 1)
        {
            m_StormTilesArea.Clear();
            m_StormOrigin = spellTiles.FirstOrigin;
            base.Trigger(spellData, spellTiles, castInfo, efficiency);
            m_StormArea.SetPossibleCastPosition(m_StormTilesArea);
        }

        protected override void TileHit(Vector2Int tilePosition, TriggerSpellData spellData)
        {
            m_StormTilesArea.Add(tilePosition);

            if (tilePosition == m_StormOrigin)
                base.TileHit(tilePosition, spellData);
        }

        protected override SpellMapPlaceable CreatePlaceable()
        {
            m_StormArea = base.CreatePlaceable() as LightningStormPlaceable;
            return m_StormArea;
        }
    }
}