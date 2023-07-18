using KarpysDev.Script.Utils;
using UnityEditor;
using UnityEngine;

namespace KarpysDev.Script.Editor
{
    [CustomPropertyDrawer(typeof(WeightElement<>))]
    public class WeightElementPropertyDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            SerializedProperty objectField = property.FindPropertyRelative("m_Object");
            SerializedProperty weightField = property.FindPropertyRelative("m_Weight");
            label.text = "";

            if (IsObjectField(property))
            {
                Rect pos = EditorGUI.PrefixLabel(position, label);
                Rect p1 = pos,p2 = pos;
                p1.width = pos.width * .4f;
                p2.xMin += p1.width;
                EditorGUI.PropertyField(p1, objectField, GUIContent.none);
                EditorGUI.PropertyField(p2, weightField, GUIContent.none);
            }
            else
            {
                position.height = EditorGUI.GetPropertyHeight(objectField);
                EditorGUI.PropertyField(position, objectField, GUIContent.none, true);
                
                position.y += position.height;
                position.height = 17;
                EditorGUI.PropertyField(position, weightField, GUIContent.none, true);
            }

        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            if (IsObjectField(property))
            {
                return 17;
            }
            else
            {
                SerializedProperty objectField = property.FindPropertyRelative("m_Object");
                SerializedProperty weightField = property.FindPropertyRelative("m_Weight");
                float size = EditorGUI.GetPropertyHeight(objectField) + EditorGUI.GetPropertyHeight(weightField);
                return size;
            }
        }

        private bool IsObjectField(SerializedProperty property)
        {
            var objectField = property.FindPropertyRelative("m_Object");

            if (objectField.propertyType == SerializedPropertyType.ObjectReference)
            {
                return true;
            }

            return false;
        }
    }
}