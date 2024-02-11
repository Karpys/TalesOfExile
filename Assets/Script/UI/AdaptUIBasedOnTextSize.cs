namespace KarpysDev.Script.UI
{
    using TMPro;
    using UnityEngine;

    public class AdaptUIBasedOnTextSize : MonoBehaviour
    {
        [SerializeField] private Vector2 m_MinDimension = Vector2.zero;
        [SerializeField] private Vector2 m_AdditionalSize = Vector2.zero;
        [SerializeField] private TMP_Text m_TargetText = null;
        [SerializeField] private RectTransform m_Transform = null;
        [SerializeField] private UpdateTiming m_UpdateTiming = UpdateTiming.Custom;

        [Header("Parameters")] 
        [SerializeField] private bool m_AdaptWidth = false;
        [SerializeField] private bool m_AdaptHeight = false;

        private void Awake()
        {
            if (m_UpdateTiming == UpdateTiming.OnAwake)
                AdaptSize();
        }

        private void Start()
        {
            if (m_UpdateTiming == UpdateTiming.OnStart)
                AdaptSize();
        }

        private void OnValidate()
        {
            if(m_TargetText && m_Transform)
                AdaptSize();
        }

        public void AdaptSize()
        {
            Vector2 targetSize = m_Transform.sizeDelta;

            if (m_AdaptWidth)
                targetSize.x = Mathf.Max(m_MinDimension.x, m_TargetText.preferredWidth + m_AdditionalSize.x);
            
            if (m_AdaptHeight)
                targetSize.y = Mathf.Max(m_MinDimension.y, m_TargetText.preferredHeight + m_AdditionalSize.y);

            m_Transform.sizeDelta = targetSize;
        }
    }
}