using UnityEngine;

public class  SpawnEntityTrigger : SelectionSpellTrigger
{
    private EntityType m_EntityTypeToSpawn = EntityType.None;
    private bool m_UseTransmitter = false;
    private int m_SpawnPriority = 0;
    public SpawnEntityTrigger(BaseSpellTriggerScriptable baseScriptable,EntityType entityType,bool useTransmitter,int spawnPriority) : base(baseScriptable)
    {
        m_EntityTypeToSpawn = entityType;
        m_UseTransmitter = useTransmitter;
        m_SpawnPriority = spawnPriority;
    }

    public override void ComputeSpellPriority()
    {
        m_SpellPriority = m_SpawnPriority;
    }

    protected override void TileHit(Vector2Int tilePosition, TriggerSpellData spellData)
    {
        base.TileHit(tilePosition, spellData);

        Tile tile = MapData.Instance.GetTile(tilePosition);

        Debug.Log(tilePosition);
        if (tile.Walkable)
        {
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