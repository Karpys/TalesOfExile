using KarpysDev.Script.Map_Related;
using KarpysDev.Script.Spell.SpellFx;
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

        protected override SpellAnimation CreateOnHitFx(Vector3 entityPosition, Transform transform)
        {
            SpellAnimation spellAnim = base.CreateOnHitFx(m_OriginPosition, transform);

            if (spellAnim is IProjectileAnim projectileAnim)
            {
                projectileAnim.StartPosition = m_OriginPosition;
                projectileAnim.EndPosition = entityPosition;
            }
            else
            {
                Debug.LogError("Consider using a projectile animation");
            }

            return spellAnim;
        }

        protected override SpellAnimation CreateTileHitFx(Vector3 tilePosition, Transform transform)
        {
            SpellAnimation spellAnim = base.CreateTileHitFx(m_OriginPosition, transform);

            if (spellAnim is IProjectileAnim projectileAnim)
            {
                projectileAnim.StartPosition = m_OriginPosition;
                projectileAnim.EndPosition = tilePosition;
            }else
            {
                Debug.LogError("Consider using a projectile animation");
            }

            return spellAnim;
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