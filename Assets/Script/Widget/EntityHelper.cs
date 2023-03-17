public static class EntityHelper
{
    public static EntityGroup GetInverseEntityGroup(EntityGroup entityGroup)
    {
        if (entityGroup == EntityGroup.Enemy)
            return EntityGroup.Friendly;
        if (entityGroup == EntityGroup.Friendly)
            return EntityGroup.Enemy;
        return EntityGroup.Neutral;
    }
}
