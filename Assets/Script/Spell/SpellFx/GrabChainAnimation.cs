using System.Collections;
using KarpysDev.Script.Widget;
using TweenCustom;
using UnityEngine;

namespace KarpysDev.Script.Spell.SpellFx
{
    public class GrabChainAnimation : BurstAnimation
    {
        [SerializeField] private PointLineRenderer m_LineRenderer = null;
        [Header("Movement Parameters")]
        [SerializeField] private AnimationCurve m_MovementCurve = null;
        [SerializeField] private float m_MovementDuration = 0.2f;
        [SerializeField] private float m_GrabDelay = 0.1f;
        
        private Transform m_EntityHitTransform = null;
        private Transform m_CasterPosition = null;
        private Vector3 m_TargetPosition = Vector3.zero;
        private PointLineRenderer lineRenderer = null;
        protected override void Start()
        {
            if (m_Datas == null)
            {
                Debug.LogError("Try anim line renderer without additional datas");
                return;
            }
            
            m_EntityHitTransform = (Transform)m_Datas[0];
            m_CasterPosition = (Transform)m_Datas[1];
            m_TargetPosition = (Vector3)m_Datas[2];
            base.Start();
        }

        protected override void Animate()
        { 
            lineRenderer = Instantiate(m_LineRenderer, m_EntityHitTransform.position,Quaternion.identity);
            lineRenderer.Initialize(m_CasterPosition,m_EntityHitTransform);
            m_EntityHitTransform.DoMove(m_TargetPosition, m_MovementDuration).SetCurve(m_MovementCurve).SetDelay(m_GrabDelay).OnComplete(Break).OnReferenceLose(Break);
        }

        private void Break()
        {
            if(lineRenderer)
                lineRenderer.Break();
            
            Destroy(gameObject);
        }
    }
}