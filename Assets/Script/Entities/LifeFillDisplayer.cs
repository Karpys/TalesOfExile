namespace KarpysDev.Script.Entities
{
    using KarpysUtils.TweenCustom;
    using UnityEngine;
    using Widget;

    public class LifeFillDisplayer : MonoBehaviour,ILifeDisplayer
    {
        [SerializeField] private BoardEntityLife m_BoardEntityLife = null;
        [SerializeField] private Transform m_Visual = null;
        [SerializeField] private SpriteRenderer m_LifeFill = null;
        [SerializeField] private Transform m_LifeDamageEffect = null;

        public BoardEntityLife EntityLife => m_BoardEntityLife;
        public void EnableDisplay()
        {
            m_Visual.gameObject.SetActive(true);
            UpdateLifeDisplay();
        }

        public void DisableDisplay()
        {
            m_Visual.gameObject.SetActive(false);
        }

        private BaseTween m_CurrentTween = null;

        public void UpdateLifeDisplay()
        {
            float ratio = EntityLife.Life / EntityLife.MaxLife;
            m_LifeFill.color = ColorHelper.GetLifeLerp(ratio);

            Vector3 targetScale = new Vector3(1, ratio, 1);
            m_LifeFill.transform.localScale = targetScale;

            if (m_CurrentTween != null)
            {
                if (m_CurrentTween.IsComplete)
                {
                    //Reset
                    m_CurrentTween.TweenRefreshStartValue();
                    m_CurrentTween.EndValue = targetScale;
                    m_CurrentTween.SetDelay(0.5f);
                    m_CurrentTween.Reset();
                    //Add
                    TweenManager.Instance.AddTween(m_CurrentTween);
                }
                else
                {
                    //Reset
                    m_CurrentTween.TweenRefreshStartValue();
                    m_CurrentTween.EndValue = targetScale;
                    m_CurrentTween.SetDelay(0.5f);
                    m_CurrentTween.Reset();
                }
            }
            else
            {
                m_CurrentTween = m_LifeDamageEffect.DoScale(targetScale, 0.25f).SetDelay(.5f).SetEase(Ease.EASE_OUT_SIN);
            }
        }

        public void UpdateShieldDisplay()
        {
            return;
        }

        public void EnableShieldDisplay()
        {
            return;
        }

        public void HideShieldDisplay()
        {
            return;
        }
    }
}