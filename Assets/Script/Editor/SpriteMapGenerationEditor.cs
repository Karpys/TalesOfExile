using System;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(SpriteMapGeneration))]
public class SpriteMapGenerationEditor:Editor
{
    private SpriteMapGeneration m_Target = null;

    public void OnEnable()
    {
        m_Target = target as SpriteMapGeneration;
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        AddGenerateLibraryButton();
    }

    private void AddGenerateLibraryButton()
    {
        if (GUILayout.Button("Generate Librarty"))
        {
            m_Target.GenerateLibrary(ColorHelper.GetColorInSprite(m_Target.MapSprite));
        }
    }
}
