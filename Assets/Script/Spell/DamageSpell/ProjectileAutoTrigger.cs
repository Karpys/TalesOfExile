using UnityEngine;

public class ProjectileAutoTrigger : WeaponDamageTrigger
{
    private Vector3 m_OriginPosition = Vector3.zero;
    public ProjectileAutoTrigger(DamageSpellScriptable damageSpellData, float baseWeaponDamageConvertion) : base(damageSpellData, baseWeaponDamageConvertion)
    {}
    
    public override void Trigger(TriggerSpellData spellData, SpellTiles spellTiles, CastInfo castInfo)
    {
        m_OriginPosition = MapData.Instance.GetTilePosition(spellTiles.CenterOrigin);
        base.Trigger(spellData, spellTiles, castInfo);
    }

    protected override void TriggerOnHitFx(Vector3 entityPosition, Transform transform, params object[] args)
    {
        base.TriggerOnHitFx(m_OriginPosition, transform,m_OriginPosition,entityPosition);
    }

    protected override void TriggerTileHitFx(Vector3 tilePosition, Transform transform, params object[] args)
    {
        base.TriggerTileHitFx(m_OriginPosition, transform, m_OriginPosition,tilePosition);
    }
}