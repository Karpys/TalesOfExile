using UnityEngine;

namespace KarpysDev.Script.Spell.SpellFx
{
    using KarpysUtils.TweenCustom;

    public class FxGrabChainAnimation : FxBurstAnimation,IPointAttach
    {
        [SerializeField] private PointLineRenderer m_LineRenderer = null;
        [Header("Movement Parameters")]
        [SerializeField] private AnimationCurve m_MovementCurve = null;
        [SerializeField] private float m_MovementDuration = 0.2f;
        [SerializeField] private float m_GrabDelay = 0.1f;
        
        private Transform m_EntityHitTransform = null;
        private Transform m_CasterTransform = null;
        private Vector3 m_TargetPosition = Vector3.zero;
        private PointLineRenderer lineRenderer = null;
        
        public Transform TargetTransform {set => m_EntityHitTransform = value;}
        public Transform CasterTransform {set => m_CasterTransform = value;}
        public Vector3 TargetPosition { set => m_TargetPosition = value;}
        protected override void Animate()
        { 
            lineRenderer = Instantiate(m_LineRenderer, m_EntityHitTransform.position,Quaternion.identity);
            lineRenderer.Initialize(m_CasterTransform,m_EntityHitTransform);
            m_EntityHitTransform.DoMove(m_TargetPosition, m_MovementDuration).SetCurve(m_MovementCurve).SetDelay(m_GrabDelay).OnComplete(Break).OnReferenceLose(Break);
        }

        private void Break()
        {
            if(lineRenderer)
                lineRenderer.Break();
            
            Destroy(gameObject);
        }
    }

    public interface IPointAttach
    {
        Transform TargetTransform { set; }
        Transform CasterTransform { set; }
        Vector3 TargetPosition { set; }
    }
}