using System.Collections;
using System.Collections.Generic;
using KarpysDev.Script.Utils;
using UnityEngine;

namespace KarpysDev.Script.Spell.SpellFx
{
    using KarpysUtils.TweenCustom;

    public class Fx_ProjectileSequence : Fx_BurstAnimation,ISplitter
    {
        [SerializeField] private SpriteRenderer m_Visual = null;
        [SerializeField] private Vector2 ProjectileSpeedReference = new Vector2(5, 0.2f);

        private Vector3[] m_Points = null;
    
        public Vector3[] SplitTargets {set => m_Points = value;}
        
        protected override float GetAnimationDuration()
        {
            return ProjectileSpeedReference.y;
        }

        protected override void Animate()
        {
            bool isLast = false;
            StartCoroutine(CO_Sequence());
        
            IEnumerator CO_Sequence()
            {
                for (int i = 0; i < m_Points.Length; i++)
                {
                    if (i == m_Points.Length - 1)
                        isLast = true;
            
                    float arrowSpeed = Vector3.Distance(transform.position, m_Points[i]) * ProjectileSpeedReference.y / ProjectileSpeedReference.x;
                    SpriteUtils.RotateTowardPoint(transform.position, m_Points[i], m_Visual.transform);
                
                    if (isLast)
                    {
                        transform.DoMove(m_Points[i], arrowSpeed).OnComplete(() => Destroy(gameObject));
                    }
                    else
                    {
                        transform.DoMove(m_Points[i], arrowSpeed);
                    }
                
                    yield return new WaitForSeconds(arrowSpeed);
                }
            }
        }
    }
}