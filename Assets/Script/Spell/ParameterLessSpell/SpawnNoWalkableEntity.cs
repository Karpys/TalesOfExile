public class SpawnNoWalkableEntity : SpawnEntityTrigger
{
    public SpawnNoWalkableEntity(BaseSpellTriggerScriptable baseScriptable, EntityType entityType) : base(baseScriptable, entityType)
    {}

    protected override BaseEntityIA GetEntityIa()
    {
        return new BalistaIA();
    }
}