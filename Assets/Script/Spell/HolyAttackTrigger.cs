using KarpysDev.Script.Map_Related;
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
            base.TriggerTileHitFx(MapData.Instance.GetTilePosition(spellTiles.CenterOrigin), null, null);
            base.Trigger(spellData, spellTiles, castInfo, efficiency);
        }

        protected override void TriggerTileHitFx(Vector3 tilePosition, Transform transform, params object[] args)
        {
            return;
        }
    }
}