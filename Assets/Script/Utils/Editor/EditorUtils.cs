using System;
using System.Globalization;
using UnityEditor;
using Object = UnityEngine.Object;

namespace KarpysDev.Script.Utils.Editor
{
    public static class EditorUtils
    {
        public static string ToPath(this DefaultAsset folder)
        {
            return AssetDatabase.GetAssetPath(folder);
        }
        
    }
}
