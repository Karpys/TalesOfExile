namespace KarpysDev.Script.Spell.ParameterLessSpell
{
    using Manager.Library;
    using Map_Related;
    using UnityEngine;

    public class CreateLifePlaceableTrigger : CreatePlaceableTrigger
    {
        private int m_Turn = 0;
        
        public CreateLifePlaceableTrigger(BaseSpellTriggerScriptable baseScriptable, PlaceableType placeableType,int turn) : base(baseScriptable, placeableType)
        {
            m_Turn = turn;
        }

        protected override MapPlaceable CreatePlaceable(Vector2Int tilePosition)
        {
            MapPlaceable tile = base.CreatePlaceable(tilePosition);

            if (tile is ITurn lifeTile)
            {
                lifeTile.SetTurn(m_Turn);
            }

            return tile;
        }
    }
}