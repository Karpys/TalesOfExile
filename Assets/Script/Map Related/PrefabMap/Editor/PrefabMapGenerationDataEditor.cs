namespace KarpysDev.Script.Map_Related.PrefabMap
{
    using MapGeneration;
    using UnityEditor;
    using UnityEngine;

    [CustomEditor(typeof(PrefabMapGenerationData))]
    public class PrefabMapGenerationDataEditor : Editor
    {
        private PrefabMapGenerationData m_Target = null;
        private void OnEnable()
        {
            m_Target = target as PrefabMapGenerationData;
        }

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            AddUpdateData();
        }
        
        private void AddUpdateData()
        {
            if (GUILayout.Button("Update Data"))
            {
                m_Target.UpdateData();
            }
        }
    }
}