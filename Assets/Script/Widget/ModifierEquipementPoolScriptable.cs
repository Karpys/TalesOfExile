using UnityEngine;

namespace Script.Widget
{
    [CreateAssetMenu(fileName = "ModifierEquipementPool", menuName = "Modifier/ModifierEquipementPool", order = 0)]
    public class ModifierEquipementPoolScriptable : ScriptableObject
    {
        public GenericObjectLibrary<ModifierPoolScriptable, EquipementType> m_EquipementModifierPool = null;
        //public ModifierPool m_ModifierPool = null;
    }
}