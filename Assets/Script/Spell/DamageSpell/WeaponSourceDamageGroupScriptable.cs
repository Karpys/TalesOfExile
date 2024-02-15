namespace KarpysDev.Script.Spell.DamageSpell
{
    using System.Collections.Generic;
    using UnityEngine;

    [CreateAssetMenu(fileName = "SourceDamageGroup", menuName = "Damage/Weapon Damage Group", order = 0)]
    public class WeaponSourceDamageGroupScriptable : BaseSourceDamageGroup
    {
        [SerializeField] private WeaponDamageSource m_InitialWeaponDamage = null;

        public override List<DamageSource> Init()
        {
            List<DamageSource> damageSources = new List<DamageSource>();
            damageSources.Add(new WeaponDamageSource(m_InitialWeaponDamage));
            return damageSources;
        }
    }
}