using System;
using System.Collections.Generic;
using System.Linq;
using KarpysDev.KarpysUtils;
using KarpysDev.KarpysUtils.AutoFielder;
using KarpysDev.Script.Spell;
using KarpysDev.Script.Utils.Editor;
using UnityEditor;
using UnityEngine;

namespace KarpysDev.Script.Editor
{
    public class SpellBuyableCreatorWindow : EditorWindow
    {
        [SerializeField] private List<TriggerSpellDataScriptable> m_SpellsToCopy = new List<TriggerSpellDataScriptable>();
        [SerializeField] private DefaultAsset m_AssetPath = null;
        [MenuItem("Tools/SpellBuyableWindow")]
        private static void ShowWindow()
        {
            var window = GetWindow<SpellBuyableCreatorWindow>();
            window.titleContent = new GUIContent("Spell Buyable Creator");
            window.Show();
        }

        private void OnGUI()
        {
            SerializedObject serializedObject = new SerializedObject(this);
            
            SerializedProperty spellsProperty = serializedObject.FindProperty("m_SpellsToCopy");
            EditorGUILayout.PropertyField(spellsProperty, true);
            
            SerializedProperty assetPath = serializedObject.FindProperty("m_AssetPath");
            EditorGUILayout.PropertyField(assetPath, true);


            if (GUILayout.Button("Create Buyable"))
            {
                string folderPath = m_AssetPath.ToPath();
                for (int i = 0; i < m_SpellsToCopy.Count; i++)
                {
                    CreateSpellBuyable(m_SpellsToCopy[i],folderPath);        
                }
                
            }
            
            serializedObject.ApplyModifiedProperties();
        }

        private void CreateSpellBuyable(TriggerSpellDataScriptable scriptable,string folderPath)
        {
            if(scriptable == null)
                return;
            SpellBuyableScriptable newBuyable = CreateInstance<SpellBuyableScriptable>();
            newBuyable.SetSpellData(scriptable);
            string spellName = string.Concat(scriptable.name.Where(c => !char.IsWhiteSpace(c)));
            AssetDatabase.CreateAsset(newBuyable,folderPath+"/"+"Buyable"+spellName+".asset");
        }
    }
    
    public class ReplaceAssemblyNameInSpells : EditorWindow
    {
        [SerializeField] private List<BaseSpellTriggerScriptable> m_SpellsData = new List<BaseSpellTriggerScriptable>();
        [MenuItem("Tools/Replace Assembly Names")]
        private static void ShowWindow()
        {
            var window = GetWindow<ReplaceAssemblyNameInSpells>();
            window.titleContent = new GUIContent("Replace Assembly Name");
            window.Show();
        }

        private void OnGUI()
        {
            SerializedObject serializedObject = new SerializedObject(this);
            
            SerializedProperty spellsProperty = serializedObject.FindProperty("m_SpellsData");
            EditorGUILayout.PropertyField(spellsProperty, true);

            if (GUILayout.Button("Rename"))
            {
                foreach (BaseSpellTriggerScriptable baseSpellTriggerScriptable in m_SpellsData)
                {
                    if (baseSpellTriggerScriptable is IFielder fielder)
                    {
                        foreach (FieldValue fieldValue in fielder.Fielder.FieldValues)
                        {
                            string newName = fieldValue.AssemblyQualifiedName;
                            newName = newName.Replace("Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null",
                                "TOEAsmdef, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null");
                            fieldValue.SetAssemblyQualifiedName(newName);
                        }
                        EditorUtility.SetDirty(baseSpellTriggerScriptable);
                    }
                }
            }
            
            AssetDatabase.SaveAssets();
            serializedObject.ApplyModifiedProperties();
        }
    }
}