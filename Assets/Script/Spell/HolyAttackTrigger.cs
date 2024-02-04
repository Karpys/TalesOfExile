using KarpysDev.Script.Map_Related;
using KarpysDev.Script.Spell.SpellFx;
using UnityEngine;

namespace KarpysDev.Script.Spell
{
    //Obsolete => used for origin spell animation
    public class HolyAttackTrigger : HealSelectionSpellTrigger
    {
        public HolyAttackTrigger(BaseSpellTriggerScriptable baseScriptable, float healValue) : base(baseScriptable, healValue)
        {}

        protected override void Trigger(TriggerSpellData spellData, SpellTiles spellTiles, CastInfo castInfo, float efficiency = 1)
        {     
            base.CreateTileHitFx(MapData.Instance.GetTilePosition(spellTiles.CenterOrigin), null);
            base.Trigger(spellData, spellTiles, castInfo, efficiency);
        }

        protected override SpellAnimation CreateTileHitFx(Vector3 tilePosition, Transform transform)
        {
            return null;
        }
    }
}