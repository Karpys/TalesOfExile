
namespace KarpysDev.Script.Spell
{
    using System;
    using KarpysUtils;
    using KarpysUtils.AutoFielder;
    using UnityEngine;
    using Object = UnityEngine.Object;

    [CreateAssetMenu(fileName = "ParameterLessTrigger", menuName = "Trigger/ParameterLessTrigger", order = 0)]
    public class ParameterLessTrigger : BaseSpellTriggerScriptable,IFielder
    {
        [SerializeField] private string m_TriggerClassName = string.Empty;
        [SerializeField] private Fielder m_AdditionalParameters = null;

        public Fielder Fielder => m_AdditionalParameters;
        public Object TargetObject => this;
        
        private object[] m_FieldValues = null;
        public void GenerateFields()
        {
            m_FieldValues = m_AdditionalParameters.GetFields();
        }

        //End editor
        public override BaseSpellTrigger SetUpTrigger()
        {
            if(m_FieldValues == null)
                GenerateFields();
            
            string className = m_TriggerClassName;
            Type triggerClass = StringUtils.GetTypeViaClassName(className);
        
            if(triggerClass == null)
                Debug.LogError("The class : " + m_TriggerClassName + " is not recognized");
        
            object[] attributes = new object[m_FieldValues.Length + 1];

            attributes[0] = this;

            for (int i = 0; i < m_FieldValues.Length; i++)
            {
                attributes[i + 1] = m_FieldValues[i];
            }
        
            return (BaseSpellTrigger)Activator.CreateInstance(triggerClass,attributes);
        }
    }
}