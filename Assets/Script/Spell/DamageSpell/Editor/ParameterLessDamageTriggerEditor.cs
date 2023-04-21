using System;
using UnityEditor;
using UnityEngine;


[CustomEditor(typeof(ParameterLessDamageTriggerScriptable))]
public class ParameterLessDamageTriggerScriptableEditor : Editor
{
    private ParameterLessDamageTriggerScriptable m_ParameterLessTrigger = null;
    private string m_className = string.Empty;

    private string[] m_FieldsName = null;
    private FieldValue[] m_FieldValues = null;
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        EditorGUILayout.Space(10);
        
        if(m_FieldsName == null || m_FieldsName.Length == 0)
            return;

        for (int i = 0; i < m_FieldsName.Length; i++)
        {
            AddField(m_FieldValues[i],i);
        }
        
        if(GUILayout.Button("Send values"))
        {
            m_ParameterLessTrigger.AdditionalParameters = new FieldValue[m_FieldValues.Length];
            
            for (int i = 0; i < m_FieldValues.Length; i++)
            {
                FieldValue fieldValue = m_FieldValues[i];
                m_ParameterLessTrigger.AdditionalParameters[i] = new FieldValue(fieldValue.Type, fieldValue.Value);
            }
            
            EditorUtility.SetDirty(target);
            serializedObject.ApplyModifiedProperties();
        }
    }

    private void OnEnable()
    {
        m_ParameterLessTrigger = target as ParameterLessDamageTriggerScriptable;
        m_className = m_ParameterLessTrigger.TriggerClassName + ",Assembly-CSharp";

        Type triggerClass = StringUtils.GetTypeViaClassName(m_className);
        
        if (triggerClass != null)
        {
            StringUtils.GetConstructorsFields(triggerClass, out m_FieldsName, out m_FieldValues, 1, 1);

            if (m_ParameterLessTrigger.AdditionalParameters.Length > 0)
            {
                for (int i = 0; i < m_FieldsName.Length; i++)
                {
                    m_FieldValues[i].Value = m_ParameterLessTrigger.AdditionalParameters[i].Value;
                }
            }
        }
    }

    private void AddField(FieldValue field,int id)
    {
        EditorGUILayout.LabelField(m_FieldsName[id]);

        switch (field.Type)
        {
            case FieldType.Int:
                field.Value = EditorGUILayout.IntField(int.Parse(field.Value)).ToString();
                break;
            case FieldType.Float:
                field.Value = EditorGUILayout.FloatField(float.Parse(field.Value)).ToString();
                break;
            case FieldType.String:
                field.Value = EditorGUILayout.TextField(field.Value);
                break;
            case FieldType.Enum:
                EnumFieldValue enumFieldValue = field as EnumFieldValue;
                Enum targetEnum = Enum.Parse(enumFieldValue.EnumType,enumFieldValue.Value.Split()[1]) as Enum;

                enumFieldValue.Value = enumFieldValue.EnumType + " " + EditorGUILayout.EnumPopup(targetEnum).ToString();
                break;
        }
    }
}