using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace KarpysDev.Script.Map_Related.QuestRelated
{
    public class QuestModifierUIHolder : MonoBehaviour
    {
        [SerializeField] private Image m_Icon = null;
        [SerializeField] private TMP_Text m_BottomDescription = null;

        public void Init(QuestModifier questModifier)
        {
            m_Icon.sprite = QuestLibrary.Instance.GetIcon(questModifier.QuestModifierType);
            m_BottomDescription.text = questModifier.BottomDescription;
        }
    }
}