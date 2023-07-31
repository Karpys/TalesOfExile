using System;
using System.Globalization;
using UnityEngine;

namespace KarpysDev.Script.Map_Related.QuestRelated
{
    [Serializable]
    public class QuestModifier
    {
        [SerializeField] private QuestModifierType m_QuestModifierType = QuestModifierType.IncreaseMonsterMaxLife;
        [SerializeField] private string m_ModifierValue = String.Empty;

        private string m_BottomDescription = String.Empty;
        private string m_HoverDescription = String.Empty;
        private float m_FloatValue = 0;
        private bool m_IsFloat = false;
        public QuestModifierType QuestModifierType => m_QuestModifierType;
        public string BottomDescription => m_BottomDescription;
        public bool IsFloat => m_IsFloat;
        public float FloatValue => m_FloatValue;
        
        public QuestModifier(QuestModifier questModifier,float percentDifficulty)
        {
            m_QuestModifierType = questModifier.QuestModifierType;
            m_ModifierValue = questModifier.m_ModifierValue;
            
            m_IsFloat = float.TryParse(m_ModifierValue,NumberStyles.AllowDecimalPoint,CultureInfo.InvariantCulture,out float result);
            if(m_IsFloat)
                m_FloatValue = float.Parse(m_ModifierValue,CultureInfo.InvariantCulture);
            ApplyDifficulty(percentDifficulty);
            
            m_BottomDescription = GetBottomDescription();
            //Todo:In quest modifier utils get description//
            m_HoverDescription = GetBottomDescription();
        }
        
        public void ApplyDifficulty(float percentDifficulty)
        {
            if (IsFloat)
            {
                float floatModifierValue = m_FloatValue;
                floatModifierValue *= percentDifficulty / 100;
                Debug.Log(floatModifierValue + "float value");
                m_ModifierValue = floatModifierValue.ToString(CultureInfo.InvariantCulture);
                Debug.Log(m_ModifierValue);
                m_FloatValue = float.Parse(m_ModifierValue,CultureInfo.InvariantCulture);
            }
            else
            {
                //Switch to interpret custom string modifier values...
            }
        }

        public string GetBottomDescription()
        {
            if (IsFloat)
            {
                string floatValue = m_FloatValue.ToString("0");
                return "+" + floatValue + "%";
            }
            //Todo: switch statement with custom string modifier//
            return "";
        }
    }
}