using UnityEngine;

public class SpawnEntityTrigger : SelectionSpellTrigger
{
    private EntityType m_EntityTypeToSpawn = EntityType.None;
    public SpawnEntityTrigger(BaseSpellTriggerScriptable baseScriptable,EntityType entityType) : base(baseScriptable)
    {
        m_EntityTypeToSpawn = entityType;
    }

    public override void ComputeSpellPriority()
    {
        m_SpellPriority = 0;
    }

    protected override void TileHit(Vector2Int tilePosition, TriggerSpellData spellData)
    {
        base.TileHit(tilePosition, spellData);

        Tile tile = MapData.Instance.GetTile(tilePosition);

        if (tile.Walkable)
        {
            EntityHelper.SpawnEntityOnMap(tilePosition,EntityLibrary.Instance.GetEntityViaKey(m_EntityTypeToSpawn)
                ,GetEntityIa(),spellData.AttachedEntity.EntityGroup,spellData.AttachedEntity.TargetEntityGroup);
        }
    }

    protected virtual BaseEntityIA GetEntityIa()
    {
        return new BaseEntityIA();
    }
}