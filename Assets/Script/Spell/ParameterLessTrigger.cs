using System;
using UnityEngine;

[CreateAssetMenu(fileName = "ParameterLessTrigger", menuName = "Trigger/ParameterLessTrigger", order = 0)]
public class ParameterLessTrigger : BaseSpellTriggerScriptable
{
    [SerializeField] private string m_TriggerClassName = string.Empty;

    public override BaseSpellTrigger SetUpTrigger()
    {
        Type triggerClass = Type.GetType(m_TriggerClassName);
        
        if(triggerClass == null)
            Debug.LogError("The class : " + m_TriggerClassName + " is not recognized");
        
        return (BaseSpellTrigger)Activator.CreateInstance(triggerClass);
    }
}