using System;
using UnityEngine;

namespace KarpysDev.Script.Map_Related.QuestRelated
{
    [Serializable]
    public class QuestModifier
    {
        [SerializeField] private QuestModifierType m_QuestModifierType = QuestModifierType.AddPercentLife;
        [SerializeField] private string m_ModifierValue = String.Empty;
        [SerializeField] private float m_ModifierFactor = 1;

        public float FloatValue(out bool success)
        {
            if (float.TryParse(m_ModifierValue, out float result))
            {
                success = true;
                return result;
            }

            success = false;
            return 0;
        }
    }
}