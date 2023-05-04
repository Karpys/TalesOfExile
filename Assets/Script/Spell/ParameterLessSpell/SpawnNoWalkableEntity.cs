public class SpawnNoWalkableEntity : SpawnEntityTrigger
{
    public SpawnNoWalkableEntity(BaseSpellTriggerScriptable baseScriptable, EntityType entityType,bool useTransmitter) : base(baseScriptable, entityType,useTransmitter)
    {}

    protected override BaseEntityIA GetEntityIa()
    {
        return new BalistaIA();
    }
}