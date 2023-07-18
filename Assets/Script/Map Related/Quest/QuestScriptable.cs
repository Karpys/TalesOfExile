using System;
using KarpysDev.Script.Map_Related.MapGeneration;
using KarpysDev.Script.Utils;
using UnityEngine;

namespace KarpysDev.Script.Map_Related.Quest
{
    [CreateAssetMenu(fileName = "Quest", menuName = "Quest/New Quest", order = 0)]
    public class QuestScriptable : ScriptableObject
    {
        [SerializeField] private string m_MapName = String.Empty;
        [SerializeField] private MultipleWeightElementDraw<MapModifier> m_MalusMapModifier = null;
        [SerializeField] private MultipleWeightElementDraw<MapModifier> m_BonusMapModifier = null;
        [SerializeField] private MapGroup m_MapGroup = null;
        [SerializeField] private float m_BaseGoldAmmount = 0;
        [SerializeField] private float m_BaseExpAmmount = 0;

        public string MapName => m_MapName;
        public MultipleWeightElementDraw<MapModifier> MalusMapModifier => m_MalusMapModifier;
        public MultipleWeightElementDraw<MapModifier> BonusMapModifier => m_BonusMapModifier;
        public MapGroup MapGroup => m_MapGroup;
    }

    [System.Serializable]
    public class MapModifier
    {
        public MapModifierType MapModifierType = MapModifierType.AddPercentLife;
        public string ModifierValue = String.Empty;
        public float ModifierFactor = 1;

        public float FloatValue(out bool success)
        {
            if (float.TryParse(ModifierValue, out float result))
            {
                success = true;
                return result;
            }

            success = false;
            return 0;
        }
    }

    public enum MapModifierType
    {
        None = 0,
        AddPercentLife = 1,
        AddPercentPhysicalResistance = 2,
    }
}