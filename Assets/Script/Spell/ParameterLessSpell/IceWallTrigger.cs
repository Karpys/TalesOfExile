using KarpysDev.Script.Manager.Library;
using KarpysDev.Script.Map_Related;
using UnityEngine;

namespace KarpysDev.Script.Spell.ParameterLessSpell
{
    public class IceWallTrigger : SelectionSpellTrigger
    {
        private TileType m_TileType = TileType.None;
    
        public IceWallTrigger(BaseSpellTriggerScriptable baseScriptable,TileType tileType) : base(baseScriptable)
        {
            m_TileType = tileType;
        }

        protected override void TileHit(Vector2Int tilePosition, TriggerSpellData spellData)
        {
            base.TileHit(tilePosition, spellData);
        
            if(!MapData.Instance.IsWalkable(tilePosition))
                return;

            WorldTile tile = TileLibrary.Instance.GetTileViaKey(m_TileType);
            MapData.Instance.Map.PlaceTileAt(tile, tilePosition.x, tilePosition.y);
        }
    }
}