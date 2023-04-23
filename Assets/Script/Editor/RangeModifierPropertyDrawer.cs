using System;
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
        int spellAdditionIndex = Array.IndexOf(enumNames, ModifierType.SpellAddition.ToString());

        if (typeProperty.enumValueIndex == spellAdditionIndex)
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

    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        return 40;
    }
}
