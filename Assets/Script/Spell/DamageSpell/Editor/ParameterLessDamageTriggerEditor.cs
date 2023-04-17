using System;
using UnityEditor;
using UnityEngine;


[CustomEditor(typeof(ParameterLessDamageTriggerScriptable))]
public class ParameterLessDamageTriggerEditor : Editor
{
    private ParameterLessDamageTriggerScriptable m_ParameterLessTrigger = null;
    private string m_className = string.Empty;

    private string[] m_FieldsName = null;
    private string[] m_Fields = null;
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        EditorGUILayout.Space(10);

        for (int i = 0; i < m_FieldsName.Length; i++)
        {
            string field = m_Fields[i];
            AddStringField(field,i);
        }
        
        
        if(m_FieldsName.Length == 0)
            return;
        
        if(GUILayout.Button("Send values"))
        {
            m_ParameterLessTrigger.AdditionalParameters = m_Fields;
        }
    }

    private void OnEnable()
    {
        m_ParameterLessTrigger = target as ParameterLessDamageTriggerScriptable;
        m_className = m_ParameterLessTrigger.TriggerClassName + ",Assembly-CSharp";

        Type triggerClass = StringUtils.GetTypeViaClassName(m_className);
        
        if (triggerClass != null)
        {
            StringUtils.GetConstructorsFields(triggerClass, out m_FieldsName, 1, 1);
            
            if (m_ParameterLessTrigger.AdditionalParameters.Length > 0)
            {
                m_Fields = new string[m_FieldsName.Length];
                for (int i = 0; i < m_FieldsName.Length; i++)
                {
                    m_Fields[i] =m_ParameterLessTrigger.AdditionalParameters[i];
                }
            }
            else
            {
                m_Fields = new string[m_FieldsName.Length];
            }
        }
    }

    private void AddStringField(string paramName,int id)
    {
        EditorGUILayout.LabelField(m_FieldsName[id]);
        m_Fields[id] = EditorGUILayout.TextField(paramName);
    }
}