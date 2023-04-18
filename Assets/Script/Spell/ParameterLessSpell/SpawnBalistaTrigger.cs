using UnityEngine;

public class SpawnBalistaTrigger : SelectionSpellTrigger
{
    public SpawnBalistaTrigger(BaseSpellTriggerScriptable baseScriptable) : base(baseScriptable)
    {
    }

    protected override void TileHit(Vector2Int tilePosition, TriggerSpellData spellData)
    {
        EntityHelper.SpawnEntityOnMap(EntityLibrary.Instance.GetEntityViaKey(EntityType.Balista), tilePosition.x,tilePosition.y, spellData.AttachedEntity.Map);
        base.TileHit(tilePosition, spellData);
    }

    public override void ComputeSpellPriority()
    {
        m_SpellPriority = 0;
    }
}