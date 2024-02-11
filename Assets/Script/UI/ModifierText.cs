namespace KarpysDev.Script.UI
{
    using TMPro;
    using UnityEngine;

    public class ModifierText : MonoBehaviour
    {
        [SerializeField] private TMP_Text m_Text = null;
        [SerializeField] private AdaptUIBasedOnTextSize m_AdaptSize = null;

        public void SetText(string text)
        {
            m_Text.text = text;
            m_AdaptSize.AdaptSize();
        }
    }
}