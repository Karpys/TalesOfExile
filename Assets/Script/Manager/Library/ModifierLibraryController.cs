using System;
using KarpysDev.Script.Entities.EquipementRelated;
using KarpysDev.Script.Utils;
using UnityEngine;

namespace KarpysDev.Script.Manager.Library
{
    [Serializable]
    public class ModifierPool
    {
        [SerializeField] private MultipleWeightElementDraw<RangeModifier> m_Modifiers = new MultipleWeightElementDraw<RangeModifier>();
        public  MultipleWeightElementDraw<RangeModifier>  Modifier => m_Modifiers;
    }

    [Serializable]
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
        Tier0 = 0,
        Tier1 = 1,
        Tier2 = 2,
        Tier3 = 3,
        Tier4 = 4,
    }
}