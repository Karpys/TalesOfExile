namespace KarpysDev.Script.Map_Related.PrefabMap
{
    using System;
    using UnityEditor;
    using UnityEngine;

    [CustomEditor(typeof(ToolMapGenerator))]
    public class ToolMapGeneratorEditor : Editor
    {
        private ToolMapGenerator m_Target = null;
        private void OnEnable()
        {
            m_Target = target as ToolMapGenerator;
        }

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            AddGenerateButton();
            AddClearButton();
        }
        
        private void AddGenerateButton()
        {
            if (GUILayout.Button("Generate"))
            {
                m_Target.Generate();
            }
        }
        
        private void AddClearButton()
        {
            if (GUILayout.Button("Clear"))
            {
                m_Target.Clear();
            }
        }
    }
}