using System;
using Script.Widget;
using UnityEngine;

public class ModifierLibraryController : SingletonMonoBehavior<ModifierLibraryController>
{
    [SerializeField] private GenericObjectLibrary<ModifierEquipementPoolScriptable,Tier> m_TierPool = null;

    private void Awake()
    {
        InitItemDictionary(m_TierPool);
    }

    private void InitItemDictionary(GenericObjectLibrary<ModifierEquipementPoolScriptable,Tier> tierPool)
    {
        tierPool.InitializeDictionary();
        
        foreach (ModifierEquipementPoolScriptable modifierPoolScriptable in m_TierPool.Dictionary.Values)
        {
            modifierPoolScriptable.m_EquipementModifierPool.InitializeDictionary();
        }
    }

    public ModifierPool GetViaKey(Tier type,EquipementType equipementType)
    {
        GenericObjectLibrary<ModifierPoolScriptable, EquipementType> equipementTypeTierPool = m_TierPool.GetViaKey(type).m_EquipementModifierPool;
        if (equipementTypeTierPool == null)
        {
            Debug.LogError("Tier Modifier Pool has not been set up: " + type);
            return null;
        }

        ModifierPool modifierPool = equipementTypeTierPool.GetViaKey(equipementType).m_ModifierPool;
        if(modifierPool == null)
            Debug.LogError("Modifier Pool has not been set up: " + type);
        
        return modifierPool;
    }
}

//Used as a wrapper for the moment
[System.Serializable]
public class ModifierPool
{
    [SerializeField] private MultipleWeightElementDraw<RangeModifier> m_Modifiers = new MultipleWeightElementDraw<RangeModifier>();
    public  MultipleWeightElementDraw<RangeModifier>  Modifier => m_Modifiers;
}

[System.Serializable]
public class RangeModifier
{
    [SerializeField] private ModifierType m_Type = ModifierType.UpCold;
    [SerializeField] private Vector2 m_Range = Vector2.zero;
    [SerializeField] private string m_Params = String.Empty;

    public ModifierType Type => m_Type;

    public Vector2 Range => m_Range;
    public string Params => m_Params;
}

public enum Tier
{
    Tier0,
    Tier1,
    Tier2,
    Tier3,
    Tier4,
}
