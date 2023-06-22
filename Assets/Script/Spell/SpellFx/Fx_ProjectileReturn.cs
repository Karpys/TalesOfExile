using KarpysDev.Script.Utils;
using TweenCustom;
using UnityEngine;

namespace KarpysDev.Script.Spell.SpellFx
{
    public class Fx_ProjectileReturn : Fx_Projectile
    {
        protected override void Animate()
        {
            float arrowSpeed = Vector3.Distance(m_StartPosition, m_EndPosition) * m_ProjectileDistanceTime.y / m_ProjectileDistanceTime.x;
            transform.DoMove(m_EndPosition, arrowSpeed).OnComplete(Return);
        }

        protected virtual void Return()
        {
            float arrowSpeed = Vector3.Distance(m_EndPosition, m_StartPosition) * m_ProjectileDistanceTime.y / m_ProjectileDistanceTime.x;
            SpriteUtils.RotateTowardPoint(m_EndPosition, m_StartPosition, m_Visual.transform,m_RotationOffset);
            transform.DoMove(m_StartPosition, arrowSpeed).OnComplete(() => Destroy(gameObject));
        }
    }
}