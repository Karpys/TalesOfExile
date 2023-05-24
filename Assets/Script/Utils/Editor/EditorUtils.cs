using System;
using UnityEditor;
using UnityEngine;
using Object = UnityEngine.Object;

public static class EditorUtils
{
    public static void DrawField(FieldValue field,string fieldName)
    {
        EditorGUILayout.LabelField(fieldName);

        switch (field.Type)
        {
            case FieldType.Int:
                field.Value = EditorGUILayout.IntField(field.Value.ToInt()).ToString();
                break;
            case FieldType.Float:
                field.Value = EditorGUILayout.FloatField(field.Value.ToFloat()).ToString();
                break;
            case FieldType.String:
                field.Value = EditorGUILayout.TextField(field.Value);
                break;
            case FieldType.Enum:
                EnumFieldValue enumFieldValue = field as EnumFieldValue;
                Enum targetEnum = Enum.Parse(enumFieldValue.EnumType,enumFieldValue.Value.Split()[1]) as Enum;

                enumFieldValue.Value = enumFieldValue.EnumType + " " + EditorGUILayout.EnumPopup(targetEnum).ToString();
                break;
            case FieldType.Bool:
                field.Value = EditorGUILayout.Toggle(bool.Parse(field.Value)).ToString();
                break;
        }
    }

    public static FieldValue[] GetNewFieldValue(FieldValue[] fieldValues,SerializedObject serializedObject,Object target)
    {
        FieldValue[] newFieldValues = new FieldValue[fieldValues.Length];
            
        for (int i = 0; i < fieldValues.Length; i++)
        {
            FieldValue fieldValue = fieldValues[i];
            newFieldValues[i] = new FieldValue(fieldValue.Type, fieldValue.Value);
        }
            
        EditorUtility.SetDirty(target);
        serializedObject.ApplyModifiedProperties();

        return newFieldValues;
    }
        
}
