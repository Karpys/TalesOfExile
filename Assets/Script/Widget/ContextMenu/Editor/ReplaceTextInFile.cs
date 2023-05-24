using System.IO;
using UnityEditor;
using UnityEngine;

namespace KarpysDev.Script.Widget.ContextMenu.Editor
{
    public static class ToolMenu
    {
        [MenuItem("Tools/ReplaceString")]
        public static void ReplaceString()
        {
            string toReplace = "m_HorizontalBearingY: 54.890625";
            string newValue = "      " + toReplace.Split()[0] + " 52";
            string path = "Assets/UI/Font/EightBit.asset";
        
            string[] lines = File.ReadAllLines(path);

            for (int i = 0; i < lines.Length; i++)
            {
                if (lines[i].Contains(toReplace))
                {
                    Debug.Log("Replace");
                    lines[i] = newValue;
                }
            }
        
            File.WriteAllLines(path,lines);
        }
    }
}
