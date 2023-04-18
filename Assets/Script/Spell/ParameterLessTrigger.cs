using System;
using UnityEngine;

[CreateAssetMenu(fileName = "ParameterLessTrigger", menuName = "Trigger/ParameterLessTrigger", order = 0)]
public class ParameterLessTrigger : BaseSpellTriggerScriptable
{
    [SerializeField] private string m_TriggerClassName = string.Empty;
    #region Editor Use Only
    [SerializeField] private FieldValue[] m_AdditionalParameters = Array.Empty<FieldValue>();

    public string TriggerClassName => m_TriggerClassName;
    public FieldValue[] AdditionalParameters
    {
        set => m_AdditionalParameters = value;
        get => m_AdditionalParameters;
    }
    #endregion

    //End editor
    public override BaseSpellTrigger SetUpTrigger()
    {
        string className = m_TriggerClassName;
        Type triggerClass = Type.GetType(className.Split(':')[0]);
        
        if(triggerClass == null)
            Debug.LogError("The class : " + m_TriggerClassName + " is not recognized");
        
        object[] attributes = new object[m_AdditionalParameters.Length + 1];

        attributes[0] = this;

        for (int i = 0; i < m_AdditionalParameters.Length; i++)
        {
            attributes[i + 1] = m_AdditionalParameters[i].GetValue();
        }
        
        return (BaseSpellTrigger)Activator.CreateInstance(triggerClass,attributes);
    }
}