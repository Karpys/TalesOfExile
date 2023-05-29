using KarpysDev.Script.Entities;
using UnityEngine;

namespace KarpysDev.Script.Spell.ParameterLessSpell
{
    public class JumpTrigger : SelectionSpellTrigger
    {
        public JumpTrigger(BaseSpellTriggerScriptable baseScriptable) : base(baseScriptable)
        {
        }
        protected override void TileHit(Vector2Int tilePosition,TriggerSpellData spellData)
        {
            base.TileHit(tilePosition,spellData);
            //Move the entity//
            spellData.AttachedEntity.MoveTo(tilePosition.x,tilePosition.y);
        }

        protected override void EntityHit(BoardEntity entity, TriggerSpellData spellData,
            Vector2Int origin, CastInfo castInfo)
        {
            base.EntityHit(entity,spellData,origin,castInfo);
            return;
        }
    }
}