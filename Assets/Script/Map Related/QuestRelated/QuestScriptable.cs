using System;
using KarpysDev.Script.Map_Related.MapGeneration;
using KarpysDev.Script.Utils;
using UnityEngine;

namespace KarpysDev.Script.Map_Related.QuestRelated
{
    [CreateAssetMenu(fileName = "Quest", menuName = "Quest/New Quest", order = 0)]
    public class QuestScriptable : ScriptableObject
    {
        [SerializeField] private string m_QuestName = String.Empty;
        [SerializeField] private MultipleWeightElementDraw<QuestModifier> m_MalusMapModifier = null;
        [SerializeField] private MultipleWeightElementDraw<QuestModifier> m_BonusMapModifier = null;
        [SerializeField] private MapGroup m_MapGroup = null;
        [SerializeField] private float m_BaseGoldAmmount = 0;
        [SerializeField] private float m_BaseExpAmmount = 0;
        [SerializeField] private Sprite m_QuestPortalIcon = null;

        public string QuestName => m_QuestName;
        public float BaseGoldAmmount => m_BaseGoldAmmount;
        public float BaseExpAmmount => m_BaseExpAmmount;
        public MultipleWeightElementDraw<QuestModifier> MalusMapModifier => m_MalusMapModifier;
        public MultipleWeightElementDraw<QuestModifier> BonusMapModifier => m_BonusMapModifier;
        public MapGroup MapGroup => m_MapGroup;
        public Sprite QuestPortalIcon => m_QuestPortalIcon;

        public Quest ToQuest(QuestDifficulty difficulty)
        {
            return new Quest(this, difficulty);
        }
    }

    [System.Serializable]
    public class QuestModifier
    {
        public QuestModifierType QuestModifierType = QuestModifierType.AddPercentLife;
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

    public enum QuestModifierType
    {
        None = 0,
        AddPercentLife = 1,
        AddPercentPhysicalResistance = 2,
    }
    
    public enum QuestDifficulty
    {
        Easy = 0,
        Medium = 1,
        Hard = 2,
        Extreme =3,
    }
}