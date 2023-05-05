public class SpawnNoWalkableEntity : SpawnEntityTrigger
{

    public SpawnNoWalkableEntity(BaseSpellTriggerScriptable baseScriptable, EntityType entityType, bool useTransmitter, int spawnPriority) : base(baseScriptable, entityType, useTransmitter, spawnPriority)
    {
    }
    protected override BaseEntityIA GetEntityIa()
    {
        return new BalistaIA();
    }

    
}