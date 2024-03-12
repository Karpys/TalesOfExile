namespace KarpysDev.Script.Spell.SpellFx
{
    using KarpysUtils.TweenCustom;
    using UnityEngine;
    using Utils;

    public class FxUnarmedStrike : FxBurstAnimation,IYoYoTransform
    {
        [SerializeField] private float m_MoveDuration = 0.1f;
        [SerializeField] private SpriteRenderer m_Hit1 = null;
        [SerializeField] private SpriteRenderer m_Hit2 = null;
        [Header("Display")]
        [SerializeField] private Ease m_EaseDisplay = Ease.LINEAR;
        [SerializeField] private float m_Delay = 0.1f;
        [SerializeField] private float m_HitDisplay = 0.1f;
        [Header("Fade")]
        [SerializeField] protected Color m_FadeColor;
        [SerializeField] protected float m_FadeDuration = 0.2f;
        [SerializeField] private Ease m_FadeEase = Ease.LINEAR;
        
        private Vector3 m_InitialPos = Vector3.zero;
        private Vector3 m_GoToPosition = Vector3.zero;
        private Transform m_TransformToMove = null;
        
        public Vector3 InitialPosition { set => m_InitialPos = value; }
        public Vector3 GoToPosition { set => m_GoToPosition = value; }
        public Transform TransformToMove { set => m_TransformToMove = value;}
        protected override void Animate()
        {
            Vector3 targetPosition = (m_GoToPosition - m_InitialPos) / 2;
            m_TransformToMove.DoLocalMove(targetPosition, m_MoveDuration / 2).OnComplete(() =>
            {
                DisplayFx();
                if(m_TransformToMove)
                    m_TransformToMove.DoLocalMove(Vector3.zero, m_MoveDuration / 2); 
            }).OnReferenceLose(DisplayFx);
        }

        private void DisplayFx()
        {
            DisplayHit(m_Hit1, 0,false);
            DisplayHit(m_Hit2, m_Delay,true);
        }

        private void DisplayHit(SpriteRenderer targetRenderer,float delay,bool destroy)
        {
            if (destroy)
            {
                targetRenderer.DoColor(Color.white, m_HitDisplay).SetDelay(delay).SetEase(m_EaseDisplay).OnComplete(() =>
                {
                    targetRenderer.FadeAndDestroy(m_FadeColor, m_FadeDuration, gameObject).SetEase(m_FadeEase);
                });
            }
            else
            {
                targetRenderer.DoColor(Color.white, m_HitDisplay).SetDelay(delay).SetEase(m_EaseDisplay).OnComplete(() =>
                {
                    targetRenderer.DoColor(m_FadeColor, m_FadeDuration).SetEase(m_FadeEase);
                });
            }
        }
    }
}