using KarpysDev.Script.Utils;
using UnityEngine;

namespace KarpysDev.Script.Spell.SpellFx
{
    using KarpysUtils.TweenCustom;

    public class Fx_ProjectileAnim : Fx_BurstAnimation,IProjectileAnim
    {
        [SerializeField] protected SpriteRenderer m_Visual = null;
    
        [Header("Parameters")]
        [SerializeField] protected Vector2 m_ProjectileDistanceTime = new Vector2(5, 0.2f);
        [SerializeField] protected float m_RotationOffset = 0;
        
        protected Vector3 m_StartPosition = Vector3.zero;
        protected Vector3 m_EndPosition = Vector3.zero;
        
        public Vector3 StartPosition {set => m_StartPosition = value;}
        public Vector3 EndPosition {set => m_EndPosition = value;}

        protected override float GetAnimationDuration()
        {
            return m_AnimationLockTime;
        }

        protected override void Start()
        {
            transform.position = m_StartPosition;
            SpriteUtils.RotateTowardPoint(m_StartPosition, m_EndPosition, m_Visual.transform,m_RotationOffset);
            base.Start();
        }

    
    
        protected override void Animate()
        {
            float arrowSpeed = Vector3.Distance(m_StartPosition, m_EndPosition) * m_ProjectileDistanceTime.y / m_ProjectileDistanceTime.x;
            transform.DoMove(m_EndPosition, arrowSpeed).OnComplete(() => Destroy(gameObject));
        }
    }

    public interface IProjectileAnim
    {
        Vector3 StartPosition { set; }
        Vector3 EndPosition { set; }
    }
}