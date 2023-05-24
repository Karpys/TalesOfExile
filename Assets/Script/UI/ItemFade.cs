using UnityEngine;
using UnityEngine.UI;

namespace KarpysDev.Script.UI
{
    public class ItemFade : FollowMouse
    {
        [SerializeField] private Image m_Sprite = null;

        public void Initialize(Sprite sprite)
        {
            m_Sprite.gameObject.SetActive(true);
            m_Sprite.sprite = sprite;
        }

        public void Clear()
        {
            m_Sprite.gameObject.SetActive(false);
        }
    }
}