using UnityEngine;

namespace KarpysDev.Script.Spell.DamageSpell
{
    [CreateAssetMenu(fileName = "SpellDamage", menuName = "Trigger/Basic Damage", order = 0)]
    public class DamageSpellScriptable : BaseSpellTriggerScriptable
    {
        [SerializeField] private BaseSourceDamageGroup initialBaseDamageDefaultSources = null;
        public BaseSourceDamageGroup InitialBaseDamageDefaultSources => initialBaseDamageDefaultSources;
    
        public override BaseSpellTrigger SetUpTrigger()
        {
            return new DamageSpellTrigger(this);
        }
    }

    //Can be created via Modifier by type
    // [Serializable]
    // public class AddDamageModifier
    // {
    //     public DamageType TargetDamageType = null;
    //     public DamageSource AddedDamageSource;
    //
    //     public DamageSource? GetAdditionalDamage(DamageType initialDamageType)
    //     {
    //         if (initialDamageType.MainDamageType == TargetDamageType.MainDamageType)
    //             return AddedDamageSource;
    //
    //         for (int i = 0; i < TargetDamageType.SubDamageTypes.Length; i++)
    //         {
    //             if (initialDamageType.SubDamageTypes.Contains(TargetDamageType.SubDamageTypes[i]))
    //                 return AddedDamageSource;
    //         }
    //
    //         return null;
    //     }
    // }
}