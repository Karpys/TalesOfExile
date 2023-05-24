using KarpysDev.Script.Entities.EntitiesBehaviour;
using KarpysDev.Script.Manager.Library;
using KarpysDev.Script.Widget;
using UnityEngine;

namespace KarpysDev.Script.Spell.ParameterLessSpell
{
    public class SpawnBalistaTrigger : SelectionSpellTrigger
    {
        public SpawnBalistaTrigger(BaseSpellTriggerScriptable baseScriptable) : base(baseScriptable)
        {
        }

        protected override void TileHit(Vector2Int tilePosition, TriggerSpellData spellData)
        {
            EntityHelper.SpawnEntityOnMap(tilePosition,EntityLibrary.Instance.GetEntityViaKey(EntityType.Balista),new BalistaIA(),spellData.AttachedEntity.EntityGroup);
            base.TileHit(tilePosition, spellData);
        }
    }
}