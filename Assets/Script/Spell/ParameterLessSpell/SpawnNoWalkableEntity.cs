using KarpysDev.Script.Entities.EntitiesBehaviour;
using KarpysDev.Script.Manager.Library;

namespace KarpysDev.Script.Spell.ParameterLessSpell
{
    public class SpawnNoWalkableEntity : SpawnEntityTrigger
    {

        public SpawnNoWalkableEntity(BaseSpellTriggerScriptable baseScriptable, EntityType entityType, bool useTransmitter) : base(baseScriptable, entityType, useTransmitter)
        {
        }
        protected override BaseEntityIA GetEntityIa()
        {
            return new BalistaIA();
        }

    
    }
}