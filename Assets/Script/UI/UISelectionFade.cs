using UnityEngine;
using UnityEngine.UI;

namespace KarpysDev.Script.UI
{
    public class UISelectionFade : FollowMouse
    {
        [SerializeField] private Transform m_Container = null;
        [SerializeField] private Image m_Sprite = null;

        public void Initialize(Sprite sprite)
        {
            m_Container.gameObject.SetActive(true);
            m_Sprite.sprite = sprite;
        }

        public void Clear()
        {
            m_Container.gameObject.SetActive(false);
        }
    }
}