using UnityEditor;

namespace KarpysDev.Script.Map_Related.QuestRelated
{
    [CustomPropertyDrawer(typeof(QuestModifier))]
    public class QuestModifierPropertyDrawer : SimplePropertyDrawer
    {
        protected override SerializedProperty[] GetSerializedProperty(SerializedProperty property)
        {
            SerializedProperty[] serializedProperties = new SerializedProperty[2];
            serializedProperties[0] = property.FindPropertyRelative("m_QuestModifierType");
            serializedProperties[1] = property.FindPropertyRelative("m_ModifierValue");
            return serializedProperties;
        }

        protected override string[] GetLabelNames()
        {
            string[] names = new string[2];
            names[0] = "Modifier";
            names[1] = "Value";
            return names;
        }
    }
}