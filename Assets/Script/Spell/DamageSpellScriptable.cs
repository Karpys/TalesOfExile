using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "Basic Damage Spell", menuName = "New Spell Trigger", order = 0)]
public class DamageSpellScriptable : BaseSpellTriggerScriptable
{
    [Header("Spell Animation")]
    public SpellAnimation OnHitAnimation = null;
    
    public DamageParameters DamageParameters = null;
    
    public override BaseSpellTrigger SetUpTrigger()
    {
        Debug.Log("Return new Damage Spell Trigger");
        return new DamageSpellTrigger(this);
    }
}

[System.Serializable]
public class Modifier
{

}

[System.Serializable]
public class AddDamageModifier:Modifier
{
    public DamageType TargetDamageType = null;
    public DamageSource AddedDamageSource = null;

    public DamageSource GetAdditionalDamage(DamageType initialDamageType)
    {
        if (initialDamageType.MainDamageType == TargetDamageType.MainDamageType)
            return AddedDamageSource;

        for (int i = 0; i < TargetDamageType.SubDamageTypes.Length; i++)
        {
            if (initialDamageType.SubDamageTypes.Contains(TargetDamageType.SubDamageTypes[i]))
                return AddedDamageSource;
        }

        return null;
    }
}