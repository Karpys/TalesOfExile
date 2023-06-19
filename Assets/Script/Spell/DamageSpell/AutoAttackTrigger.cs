using UnityEngine;

namespace KarpysDev.Script.Spell.DamageSpell
{
    public class AutoAttackTrigger : WeaponDamageTrigger
    {
        public AutoAttackTrigger(DamageSpellScriptable damageSpellData, float baseWeaponDamageConvertion) : base(damageSpellData, baseWeaponDamageConvertion)
        {
        }

        protected override void Trigger(TriggerSpellData spellData, SpellTiles spellTiles, CastInfo castInfo, float efficiency = 1)
        {
            m_StartPosition = spellData.AttachedEntity.WorldPosition;
            m_TargetTransform = spellData.AttachedEntity.VisualTransform;
        
            base.Trigger(spellData, spellTiles, castInfo,efficiency);
        }

        private Vector3 m_StartPosition = Vector3.zero;
        private Transform m_TargetTransform = null;
    
        protected override void TriggerOnHitFx(Vector3 entityPosition, Transform transform, params object[] args)
        {
            base.TriggerOnHitFx(entityPosition, transform,m_StartPosition,entityPosition,m_TargetTransform);
        }

    
    }
}