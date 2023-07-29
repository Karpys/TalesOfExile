using UnityEditor;

namespace KarpysDev.Script.Map_Related.QuestRelated
{
    [CustomPropertyDrawer(typeof(QuestModifier))]
    public class QuestModifierPropertyDrawer : SimplePropertyDrawer
    {
        protected override SerializedProperty[] GetSerializedProperty(SerializedProperty property)
        {
            SerializedProperty[] serializedProperties = new SerializedProperty[3];
            serializedProperties[0] = property.FindPropertyRelative("m_QuestModifierType");
            serializedProperties[1] = property.FindPropertyRelative("m_ModifierValue");
            serializedProperties[2] = property.FindPropertyRelative("m_ModifierFactor");
            return serializedProperties;
        }

        protected override string[] GetLabelNames()
        {
            string[] names = new string[3];
            names[0] = "Modifier";
            names[1] = "Value";
            names[2] = "Factor";
            return names;
        }
    }
}