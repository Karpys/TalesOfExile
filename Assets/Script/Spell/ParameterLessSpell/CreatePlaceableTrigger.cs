using KarpysDev.Script.Manager.Library;
using KarpysDev.Script.Map_Related;
using UnityEngine;

namespace KarpysDev.Script.Spell.ParameterLessSpell
{
    using Utils;

    public class CreatePlaceableTrigger : SelectionSpellTrigger
    {
        private PlaceableType m_PlaceableType = PlaceableType.BurningGround;
    
        public CreatePlaceableTrigger(BaseSpellTriggerScriptable baseScriptable,PlaceableType placeableType) : base(baseScriptable)
        {
            m_PlaceableType = placeableType;
        }

        protected override void TileHit(Vector2Int tilePosition, TriggerSpellData spellData)
        {
            base.TileHit(tilePosition, spellData);
        
            if(!MapData.Instance.IsWalkable(tilePosition))
                return;

            CreatePlaceable(tilePosition);
        }

        protected virtual MapPlaceable CreatePlaceable(Vector2Int tilePosition)
        {
            MapPlaceable placeable = MapHelper.InsertMapPlaceable(m_PlaceableType);
            placeable.Place(tilePosition);
            return placeable;
        }
    }
}