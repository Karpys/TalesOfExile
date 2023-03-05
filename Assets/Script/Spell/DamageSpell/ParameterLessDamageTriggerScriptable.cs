using System;
using UnityEngine;

[CreateAssetMenu(fileName = "ParameterLessDamageTrigger", menuName = "Trigger/ParameterLessDamageTrigger", order = 0)]
public class ParameterLessDamageTriggerScriptable : DamageSpellScriptable
{
    [SerializeField] private string m_TriggerClassName = string.Empty;
    
    public override BaseSpellTrigger SetUpTrigger()
    {
        string className = m_TriggerClassName;
        Type triggerClass = Type.GetType(className.Split(':')[0]);
        
        if(triggerClass == null)
            Debug.LogError("The class : " + m_TriggerClassName + " is not recognized");

        string[] spellVariation = StringUtils.ExtractParameterLessVariable(className);
        object[] attributes = new object[1+spellVariation.Length];
        
        attributes[0] = this;

        for (int i = 0; i < spellVariation.Length; i++)
        {
            attributes[i + 1] = spellVariation[i];
        }
        
        return (BaseSpellTrigger)Activator.CreateInstance(triggerClass,attributes);
    }
}