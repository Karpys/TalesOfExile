namespace KarpysDev.Script.Spell.DamageSpell
{
    using System.Collections.Generic;
    using UnityEngine;

    [CreateAssetMenu(fileName = "SourceDamageGroup", menuName = "Damage/Source Damage Group", order = 0)]
    public class DefaultSourceDamageGroupScriptable : BaseSourceDamageGroup
    {
        [SerializeField] private DamageSource[] m_InitialBaseSources = null;

        public override List<DamageSource> Init()
        {
            List<DamageSource> damageSources = new List<DamageSource>();

            foreach (DamageSource source in m_InitialBaseSources)
            {
                damageSources.Add(new DamageSource(source));
            }

            return damageSources;
        }
    }
}