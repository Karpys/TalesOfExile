using KarpysDev.Script.Items;
using KarpysDev.Script.Manager;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace KarpysDev.Script.UI
{
    public class ItemDescriptionDisplayer : MonoBehaviour
    {
        [Header("Item Base Parameters")] 
        [SerializeField] private MaskScroll m_MaskScroll = null;
        [SerializeField] private float m_ScrollTime = 0.5f;
        [SerializeField] private AdaptUISize m_SelfSize = null;
        [SerializeField] private AdaptUILayoutSize m_ContainerLayout = null;
        [SerializeField] private ContentSizeFitter m_SizeFilter = null;
    
        [Header("Name and description")]
        [SerializeField] private TMP_Text m_NameText = null;
        [SerializeField] private TMP_Text m_DescriptionText = null;
        
        public virtual void Initialize(Item item)
        {
            m_NameText.text = item.Data.ObjectName;
            m_DescriptionText.text = item.Data.Description;
            // m_SizeFilter.SetLayoutVertical();
        
            m_ContainerLayout.AdaptSize();
            m_SelfSize.UpdateSize();
            RectTransform rect = (RectTransform) transform;
            m_MaskScroll.LaunchScroll(rect.sizeDelta.y,m_ScrollTime);
            GlobalCanvas.Instance.ClampX(rect);
        }
    }
}