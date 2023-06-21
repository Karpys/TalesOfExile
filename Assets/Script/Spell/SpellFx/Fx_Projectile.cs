using KarpysDev.Script.Utils;
using TweenCustom;
using UnityEngine;

namespace KarpysDev.Script.Spell.SpellFx
{
    public class Fx_Projectile : BurstAnimation
    {
        [SerializeField] private SpriteRenderer m_Visual = null;
        [SerializeField] private Vector2 m_ProjectileDistanceTime = new Vector2(5, 0.2f);
    
        private Vector3 m_StartPosition = Vector3.zero;
        private Vector3 m_EndPosition = Vector3.zero;

        protected override float GetAnimationDuration()
        {
            return m_ProjectileDistanceTime.y;
        }

        protected override void Start()
        {
            if (m_Datas.Length == 0)
            {
                Debug.LogError("Try Launch Fx with no start / end position data");
                return;
            }
        
            m_StartPosition = (Vector3)m_Datas[0];
            m_EndPosition = (Vector3)m_Datas[1];
        
            transform.position = m_StartPosition;
            SpriteUtils.RotateTowardPoint(m_StartPosition, m_EndPosition, m_Visual.transform);
            m_Visual.enabled = true;
        
            base.Start();
        }

    
    
        protected override void Animate()
        {
            float arrowSpeed = Vector3.Distance(transform.position, m_EndPosition) * m_ProjectileDistanceTime.y / m_ProjectileDistanceTime.x;
            transform.DoMove(m_EndPosition, arrowSpeed).OnComplete(() => Destroy(gameObject));
        }
    }
}