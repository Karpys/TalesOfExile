using KarpysDev.Script.Entities;
using KarpysDev.Script.Manager.Library;
using KarpysDev.Script.Map_Related;
using KarpysDev.Script.Utils;
using UnityEngine;

namespace KarpysDev.Script.Spell
{
    public class CreateSpellPlaceableTrigger : SelectionSpellTrigger
    {
        private PlaceableType m_PlaceableType = PlaceableType.BurningGround;
        private BehaveTiming m_BehaveTiming = BehaveTiming.SameAsSource;
        private int m_BehaveCount = 0;
        public CreateSpellPlaceableTrigger(BaseSpellTriggerScriptable baseScriptable,PlaceableType placeableType,BehaveTiming behaveTiming,int behaveCount) : base(baseScriptable)
        {
            m_PlaceableType = placeableType;
            m_BehaveTiming = behaveTiming;
            m_BehaveCount = behaveCount;
        }

        protected override void TileHit(Vector2Int tilePosition, TriggerSpellData spellData)
        {
            base.TileHit(tilePosition, spellData);

            SpellMapPlaceable spellMapPlaceable = MapHelper.InsertMapPlaceable(m_PlaceableType) as SpellMapPlaceable;

            if (spellMapPlaceable == null)
            {
                Debug.Log("Target Placeable Type is not a spell Map Placeable => Return");
                return;
            }
        
            spellMapPlaceable.Initialize(spellData.AttachedEntity,tilePosition,m_BehaveTiming,m_BehaveCount);
        }
    }
}