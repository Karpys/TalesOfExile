using System;
using UnityEngine;

[CreateAssetMenu(fileName = "ParameterLessTrigger", menuName = "Trigger/ParameterLessTrigger", order = 0)]
public class ParameterLessTrigger : BaseSpellTriggerScriptable
{
    [SerializeField] private string m_TriggerClassName = string.Empty;

    public override BaseSpellTrigger SetUpTrigger()
    {
        string className = m_TriggerClassName;
        Type triggerClass = Type.GetType(className.Split(':')[0]);
        
        if(triggerClass == null)
            Debug.LogError("The class : " + m_TriggerClassName + " is not recognized");
        
        string[] spellVariation = StringUtils.ExtractParameterLessVariable(className);
        object[] attributes = new object[spellVariation.Length];

        for (int i = 0; i < spellVariation.Length; i++)
        {
            attributes[i] = spellVariation[i];
        }
        
        return (BaseSpellTrigger)Activator.CreateInstance(triggerClass,attributes);
    }
}