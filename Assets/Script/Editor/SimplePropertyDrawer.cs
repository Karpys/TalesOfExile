using System;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

namespace KarpysDev.Script.Map_Related.QuestRelated
{
    public abstract class SimplePropertyDrawer : PropertyDrawer
    {
        protected int propertyCount = 0;
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            DisplayProperty(GetSerializedProperty(property),position,label);
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            return 20 * GetPropertyCount(property);
        }

        private int GetPropertyCount(SerializedProperty property)
        {
            if (propertyCount != 0)
                return propertyCount;
            else
            {
                propertyCount = GetSerializedProperty(property).Length;
                return propertyCount;
            }
        }

        private void DisplayProperty(SerializedProperty[] properties,Rect position,GUIContent label)
        {
            string[] names = GetLabelNames();
            float baseX = position.x;
            float baseXmax = position.xMax;
            for (int i = 0; i < properties.Length; i++)
            {
                position.x = baseX;
                position.xMax = baseXmax;
                position.height = 20;
                
                if (names[i] != String.Empty)
                {
                    EditorGUI.LabelField(position, names[i]);
                    position.x += 100;
                    position.xMax -= 100;
                }
                EditorGUI.PropertyField(position, properties[i], label,true);
                position.y += position.height;
            }
        }

        protected abstract SerializedProperty[] GetSerializedProperty(SerializedProperty property);
        protected abstract string[] GetLabelNames();
    }
}