using UnityEngine;

public class SpawnBalistaTrigger : SelectionSpellTrigger
{
    public SpawnBalistaTrigger(BaseSpellTriggerScriptable baseScriptable) : base(baseScriptable)
    {
    }

    protected override void TileHit(Vector2Int tilePosition, TriggerSpellData spellData)
    {
        EntityHelper.SpawnEntityOnMap(tilePosition,EntityLibrary.Instance.GetEntityViaKey(EntityType.Balista),new BalistaIA(),spellData.AttachedEntity.EntityGroup);
        base.TileHit(tilePosition, spellData);
    }
}