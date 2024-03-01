using KarpysDev.Script.Spell.SpellFx;
using UnityEngine;

namespace KarpysDev.Script.Spell.DamageSpell
{
    using System;
    using System.Globalization;
    using Manager;
    using Utils;

    public class AutoAttackTrigger : DamageSpellTrigger
    {
        public AutoAttackTrigger(DamageSpellScriptable damageSpellData) : base(damageSpellData)
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
    
        protected override SpellAnimation CreateOnHitFx(Vector3 entityPosition, Transform transform)
        {
            SpellAnimation spellAnim = base.CreateOnHitFx(entityPosition, transform);
            if (spellAnim is IYoYoTransform yoyo)
            {
                yoyo.InitialPosition = m_StartPosition;
                yoyo.GoToPosition = entityPosition;
                yoyo.TransformToMove = m_TargetTransform;
            }
            else
            {
                Debug.LogError("Consider using a yoyo transform animation");                
            }

            return spellAnim;
        }

        public override string[] GetDescriptionParts()
        {
            WeaponDamageSource initialWeaponDamageSource = null;

            foreach (DamageSource baseDamageSource in m_BaseDamageSources)
            {
                if (baseDamageSource is WeaponDamageSource weaponDamageSource)
                {
                    initialWeaponDamageSource = weaponDamageSource;
                }
            }

            if (initialWeaponDamageSource == null)
                return Array.Empty<string>();

            string[] parts = new string[2];
            parts[0] = (initialWeaponDamageSource.ConversionPercentage * 100).ToString(CultureInfo.InvariantCulture);
            parts[1] = m_ComputedDamageSources.ToWeaponDescription(initialWeaponDamageSource.WeaponTarget, m_AttachedSpell.AttachedEntity);
            return parts;
        }
    }
}