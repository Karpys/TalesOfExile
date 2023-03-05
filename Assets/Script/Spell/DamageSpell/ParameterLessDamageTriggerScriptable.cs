using System;
using UnityEngine;

[CreateAssetMenu(fileName = "ParameterLessDamageTrigger", menuName = "Trigger/ParameterLessDamageTrigger", order = 0)]
public class ParameterLessDamageTriggerScriptable : DamageSpellScriptable
{
    [SerializeField] private string m_TriggerClassName = string.Empty;
    
    public override BaseSpellTrigger SetUpTrigger()
    {
        Type triggerClass = Type.GetType(m_TriggerClassName);
        
        if(triggerClass == null)
            Debug.LogError("The class : " + m_TriggerClassName + " is not recognized");

        object[] attributes = new object[1];
        attributes[0] = this;
        return (DamageSpellTrigger)Activator.CreateInstance(triggerClass,attributes);
    }
}