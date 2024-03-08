using KarpysDev.KarpysUtils;
using KarpysDev.Script.Map_Related;
using KarpysDev.Script.Spell.DamageSpell;
using KarpysDev.Script.Utils;
using KarpysDev.Script.Widget;
using UnityEngine;

namespace KarpysDev.Script.Spell.ParameterLessSpell
{
    public class RushTrigger : DamageSpellTrigger
    {
        public RushTrigger(DamageSpellScriptable damageSpellData) : base(damageSpellData)
        {
        }

        protected override void Trigger(TriggerSpellData spellData, SpellTiles spellTiles,CastInfo castInfo, float efficiency = 1)
        {
            MoveToClosestFreeTile(spellData,spellTiles.FirstOrigin);
            base.Trigger(spellData, spellTiles,castInfo,efficiency);
        }
    
        private void MoveToClosestFreeTile(TriggerSpellData spellData,Vector2Int position)
        {
            DistanceUtils.GetSquareDistance(spellData.AttachedEntity.EntityPosition,position).Log("Square Distance");
            if (DistanceUtils.GetSquareDistance(spellData.AttachedEntity.EntityPosition, position) > 1)
            {
                Tile closestFree = TileHelper.GetFreeClosestAround(MapData.Instance.GetTile(position),spellData.AttachedEntity.WorldPosition);
                spellData.AttachedEntity.MoveTo(closestFree.TilePosition);
            }
            else
            {
                spellData.AttachedEntity.SimulateMovement();
            }
        }
    }
}