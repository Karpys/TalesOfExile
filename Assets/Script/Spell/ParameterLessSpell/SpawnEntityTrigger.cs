using UnityEngine;

public class  SpawnEntityTrigger : SelectionSpellTrigger
{
    private EntityType m_EntityTypeToSpawn = EntityType.None;
    private bool m_UseTransmitter = false;
    public SpawnEntityTrigger(BaseSpellTriggerScriptable baseScriptable,EntityType entityType,bool useTransmitter) : base(baseScriptable)
    {
        m_EntityTypeToSpawn = entityType;
        m_UseTransmitter = useTransmitter;
    }

    public override void ComputeSpellPriority()
    {
        m_SpellPriority = 0;
    }

    protected override void TileHit(Vector2Int tilePosition, TriggerSpellData spellData)
    {
        base.TileHit(tilePosition, spellData);

        Tile tile = MapData.Instance.GetTile(tilePosition);

        Debug.Log(tilePosition);
        if (tile.Walkable)
        {
            Debug.Log("spawn entity");
            BoardEntity entity = EntityHelper.SpawnEntityOnMap(tilePosition,EntityLibrary.Instance.GetEntityViaKey(m_EntityTypeToSpawn)
                ,GetEntityIa(),spellData.AttachedEntity.EntityGroup,spellData.AttachedEntity.TargetEntityGroup);
            
            if(m_UseTransmitter)
                entity.GetComponent<StatsTransmitter>().InitTransmitter(spellData.AttachedEntity);
        }
    }

    protected virtual BaseEntityIA GetEntityIa()
    {
        return new BaseEntityIA();
    }
}