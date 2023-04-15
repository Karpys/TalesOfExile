using System;
using Script.Widget;
using UnityEngine;

public class ModifierLibraryController : SingletonMonoBehavior<ModifierLibraryController>
{
    [SerializeField] private GenericObjectLibrary<ModifierPoolScriptable,Tier> m_TierPool = null;

    private void Awake()
    {
        m_TierPool.InitializeDictionary();
    }

    public ModifierPool GetViaKey(Tier type)
    {
        ModifierPool pool = m_TierPool.GetViaKey(type).m_ModifierPool;
        
        if(pool == null)
            Debug.LogError("Modifier Pool has not been set up: " + type);
        return pool;
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

    public ModifierType Type => m_Type;

    public Vector2 Range => m_Range;
}

public enum Tier
{
    Tier0,
    Tier1,
    Tier2,
    Tier3,
    Tier4,
}
