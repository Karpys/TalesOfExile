using KarpysDev.Script.Map_Related;
using KarpysDev.Script.Spell.DamageSpell;
using KarpysDev.Script.Widget;
using UnityEngine;

namespace KarpysDev.Script.Spell.ParameterLessSpell
{
    public class RushTrigger : WeaponDamageTrigger
    {
        public RushTrigger(DamageSpellScriptable damageSpellData, float baseWeaponDamageConvertion) : base(damageSpellData, baseWeaponDamageConvertion)
        {
        }

        protected override void Trigger(TriggerSpellData spellData, SpellTiles spellTiles,CastInfo castInfo, float efficiency = 1)
        {
            MoveToClosestFreeTile(spellData,spellTiles.OriginTiles[0]);
            base.Trigger(spellData, spellTiles,castInfo,efficiency);
        }
    
        private void MoveToClosestFreeTile(TriggerSpellData spellData,Vector2Int position)
        {
            Tile closestFree = TileHelper.GetFreeClosestAround(MapData.Instance.GetTile(position),spellData.AttachedEntity.WorldPosition);
            spellData.AttachedEntity.MoveTo(closestFree.TilePosition);
        }
    }
}