using System.Collections.Generic;
using KarpysDev.Script.Utils;
using UnityEngine;

namespace KarpysDev.Script.Map_Related.QuestRelated
{
    [CreateAssetMenu(fileName = "Quest", menuName = "Quest/New Quest Modifier", order = 0)]
    public class QuestModifierScriptable : ScriptableObject
    {
        [SerializeField] private MultipleWeightElementDraw<QuestModifier> m_MapModifier = null;

        public List<QuestModifier> Draw(int count)
        {
            return m_MapModifier.MultipleDraw(count);
        }
    }
}