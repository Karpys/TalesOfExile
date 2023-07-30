using System;
using UnityEngine;

namespace KarpysDev.Script.Map_Related.QuestRelated
{
    [Serializable]
    public class QuestModifier
    {
        [SerializeField] private QuestModifierType m_QuestModifierType = QuestModifierType.IncreaseMonsterMaxLife;
        [SerializeField] private string m_ModifierValue = String.Empty;
        [SerializeField] private float m_ModifierFactor = 1;

        public QuestModifierType QuestModifierType => m_QuestModifierType;
        public bool IsFloat()
        {
            return float.TryParse(m_ModifierValue, out float result);
        }
        public float FloatValue()
        {
            if (float.TryParse(m_ModifierValue, out float result))
            {
                return result;
            }

            return 0;
        }
    }
}