using KarpysDev.Script.Map_Related;
using UnityEngine;

namespace KarpysDev.Script.Spell.DamageSpell
{
    public class ProjectileAutoTrigger : WeaponDamageTrigger
    {
        protected OriginType m_OriginType = OriginType.CasterPosition;
        protected Vector3 m_OriginPosition = Vector3.zero;

        public ProjectileAutoTrigger(DamageSpellScriptable damageSpellData, OriginType originType,
            float baseWeaponDamageConvertion) : base(damageSpellData, baseWeaponDamageConvertion)
        {
            m_OriginType = originType;
        }

        protected override void Trigger(TriggerSpellData spellData, SpellTiles spellTiles, CastInfo castInfo, float efficiency = 1)
        {
            SetOrigin(spellTiles);
            base.Trigger(spellData, spellTiles, castInfo,efficiency);
        }

        protected override void TriggerOnHitFx(Vector3 entityPosition, Transform transform, params object[] args)
        {
            base.TriggerOnHitFx(m_OriginPosition, transform,m_OriginPosition,entityPosition);
        }

        protected override void TriggerTileHitFx(Vector3 tilePosition, Transform transform, params object[] args)
        {
            base.TriggerTileHitFx(m_OriginPosition, transform, m_OriginPosition,tilePosition);
        }
        
        private void SetOrigin(SpellTiles spellTiles)
        {
            switch (m_OriginType)
            {
                case OriginType.CasterPosition:
                    m_OriginPosition = MapData.Instance.GetTilePosition(spellTiles.CenterOrigin);
                    break;
                case OriginType.FirstActionSelection:
                    m_OriginPosition = MapData.Instance.GetTilePosition(spellTiles.FirstOrigin);
                    break;
            }
        }
    }
}