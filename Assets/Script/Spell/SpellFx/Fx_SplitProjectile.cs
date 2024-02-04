using System.Collections.Generic;
using KarpysDev.Script.Utils;
using UnityEngine;

namespace KarpysDev.Script.Spell.SpellFx
{
    using KarpysUtils.TweenCustom;

    public class Fx_SplitProjectile : Fx_BurstAnimation,ISplitter
    {
        [SerializeField] private SpriteRenderer m_Visual = null;
        [SerializeField] private Fx_ProjectileAnim projectileAnimSplitAnimation = null;
        [SerializeField] private Vector2 ProjectileSpeedReference = new Vector2(5, 0.2f);
       
        private Vector3[] m_Points = null;

        public Vector3[] SplitTargets {set => m_Points = value;}

        protected override float GetAnimationDuration()
        {
            return ProjectileSpeedReference.y;
        }

        protected override void Animate()
        {
            Vector3 position = transform.position; 
            float arrowSpeed = Vector3.Distance(position, m_Points[0]) * ProjectileSpeedReference.y / ProjectileSpeedReference.x;
            SpriteUtils.RotateTowardPoint(position, m_Points[0], m_Visual.transform);
            transform.DoMove(m_Points[0], arrowSpeed).OnComplete(CreateSplit);
        }

        private void CreateSplit()
        {
            for (int i = 1; i < m_Points.Length; i++)
            {
                Fx_ProjectileAnim anim = projectileAnimSplitAnimation.TriggerFx(m_Points[0], null) as Fx_ProjectileAnim;
                anim.StartPosition = m_Points[0];
                anim.EndPosition = m_Points[i];
            }
            
            Destroy(gameObject);
        }
    }

    public interface ISplitter
    {
        public Vector3[] SplitTargets { set; }
    }
}