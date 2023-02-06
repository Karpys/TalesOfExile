public static class EntityHelper
{
    public static EntityGroup GetInverseEntityGroup(EntityGroup entityGroup)
    {
        if (entityGroup == EntityGroup.Ennemy)
            return EntityGroup.Friendly;
        if (entityGroup == EntityGroup.Friendly)
            return EntityGroup.Ennemy;
        return EntityGroup.Neutral;
    }
}
