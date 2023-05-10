using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomPropertyDrawer(typeof(RangeModifier))]
public class RangeModifierPropertyDrawer : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        EditorGUI.BeginProperty(position, label, property);

        EditorGUI.PropertyField(position, property.FindPropertyRelative("m_Type"), true);

        SerializedProperty typeProperty = property.FindPropertyRelative("m_Type");
        
        string[] enumNames = Enum.GetNames(typeof(ModifierType));

        ModifierType[] modifierTypes = new ModifierType[] {ModifierType.SpellAddition};
        
        List<int> stringParamModifier = StringParamModifierType(enumNames,modifierTypes);

        if (stringParamModifier.Contains(typeProperty.enumValueIndex))
        {
            position.y += 20;
            EditorGUI.PropertyField(position, property.FindPropertyRelative("m_Params"), true);
        }
        else
        {
            position.y += 20;
            EditorGUI.PropertyField(position, property.FindPropertyRelative("m_Range"), true);
        }
        
        EditorGUI.EndProperty();
    }

    private List<int> StringParamModifierType(string[] enumNames,ModifierType[] modifierTypes)
    {
        List<int> stringParamModifierType = new List<int>();

        foreach (ModifierType type in modifierTypes)
        {
            stringParamModifierType.Add(Array.IndexOf(enumNames, type.ToString()) + 1);
        }

        return stringParamModifierType;
    }

    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        return 40;
    }
}
