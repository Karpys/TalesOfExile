using UnityEngine;


[CreateAssetMenu(fileName = "ModifierEquipementPool", menuName = "Modifier/ModifierEquipementPool", order = 0)]
public class ModifierEquipementPoolScriptable : ScriptableObject
{
    public GenericLibrary<ModifierPoolScriptable, EquipementType> m_EquipementModifierPool = null;
    //public ModifierPool m_ModifierPool = null;
}
