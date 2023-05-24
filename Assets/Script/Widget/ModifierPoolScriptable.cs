﻿using KarpysDev.Script.Manager.Library;
using UnityEngine;

namespace KarpysDev.Script.Widget
{
    [CreateAssetMenu(fileName = "ModifierPool", menuName = "Modifier/ModifierPool", order = 0)]
    public class ModifierPoolScriptable : ScriptableObject
    {
        public ModifierPool m_ModifierPool = null;
    }
}
