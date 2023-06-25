using System;
using KarpysDev.Script.Utils;
using UnityEngine;

namespace KarpysDev.Script.Spell.SpellFx
{
    public class PointLineRenderer : MonoBehaviour
    {
        [SerializeField] private Transform m_ScaleContainer = null;
        [SerializeField] private SpriteRenderer m_TargetRenderer = null;
        [SerializeField] private float m_RotationOffset = 0; 
        
        private Transform m_PointA = null;
        private Transform m_PointB = null;

        public void Initialize(Transform pointA, Transform pointB)
        {
            m_PointA = pointA;
            m_PointB = pointB;
        }
        private void Update()
        {
            if (!m_PointA || !m_PointB)
            {
               Break();
               return;
            }
            
            transform.position = m_PointA.transform.position;
            m_ScaleContainer.eulerAngles = new Vector3(0,0,m_RotationOffset + SpriteUtils.GetRotateTowardPoint(transform.position, m_PointB.transform.position));

            float distance = Vector3.Distance(m_PointA.position, m_PointB.position);
            m_TargetRenderer.size = new Vector2(distance, 1);
            m_TargetRenderer.transform.localPosition = new Vector3(distance / 2, 0, 0);
        }

        public void Break()
        {
            Destroy(gameObject);
        }
    }
}