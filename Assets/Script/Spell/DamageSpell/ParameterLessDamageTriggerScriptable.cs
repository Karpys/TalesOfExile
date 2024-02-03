using System;
using KarpysDev.Script.Utils;
using UnityEngine;

namespace KarpysDev.Script.Spell.DamageSpell
{
    using KarpysUtils.AutoFielder;
    using FieldValue = Utils.FieldValue;
    using Object = UnityEngine.Object;

    [CreateAssetMenu(fileName = "ParameterLessDamageTrigger", menuName = "Trigger/ParameterLessDamageTrigger", order = 0)]
    public class ParameterLessDamageTriggerScriptable : DamageSpellScriptable,IFielder
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
        // #region Editor Use Only
        // [SerializeField] private FieldValue[] m_AdditionalParameters = Array.Empty<FieldValue>();
        //
        // public string TriggerClassName => m_TriggerClassName;
        // public FieldValue[] AdditionalParameters
        // {
        //     set => m_AdditionalParameters = value;
        //     get => m_AdditionalParameters;
        // }
        // #endregion
        
        public override BaseSpellTrigger SetUpTrigger()
        {
            if(m_FieldValues == null)
                GenerateFields();
            
            string className = m_AdditionalParameters.ClassName;
            Type triggerClass = StringUtils.GetTypeViaClassName(className.Split(':')[0]);
        
            if(triggerClass == null)
                Debug.LogError("The class : " + m_AdditionalParameters.ClassName + " is not recognized");

            object[] attributes = new object[1+m_FieldValues.Length];
        
            attributes[0] = this;

            for (int i = 0; i < m_FieldValues.Length; i++)
            {
                attributes[i + 1] = m_FieldValues[i];
            }
        
            return (BaseSpellTrigger)Activator.CreateInstance(triggerClass,attributes);
        }
    }
}