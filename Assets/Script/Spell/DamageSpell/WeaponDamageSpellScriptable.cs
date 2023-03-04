using UnityEngine;

[CreateAssetMenu(fileName = "WeaponDamage", menuName = "Trigger/Weapon Damage", order = 0)]
public class WeaponDamageSpellScriptable : DamageSpellScriptable
{
    [Header("Weapon Damage")]
    public float BaseWeaponDamageConvertion = 100f;
    
    public override BaseSpellTrigger SetUpTrigger()
    {
        Debug.Log("Return new Weapon Damage Spell Trigger");
        return new WeaponDamageTrigger(this);
    }
}