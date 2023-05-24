using TweenCustom;
using UnityEngine;

namespace KarpysDev.Script.Spell.SpellFx
{
    public class Fx_AutoAttack : BurstAnimation
    {
        [SerializeField] private float m_AnimDuration = 0.1f;
        [SerializeField] private SpriteRenderer m_HitFx = null;
    
        protected override float GetAnimationDuration()
        {
            return m_AnimDuration;
        }

        protected override void Animate()
        {
            base.Animate();
            Vector3 startPosition = (Vector3)m_Datas[0];
            Vector3 endPosition = (Vector3)m_Datas[1];
            Transform targetTransform = (Transform)m_Datas[2];

            Vector3 targetPosition = (endPosition + startPosition) / 2;
            targetTransform.DoMove(targetPosition, m_AnimDuration / 2).OnComplete((() =>
            {
                m_HitFx.gameObject.SetActive(true);
                targetTransform.DoMove(startPosition, m_AnimDuration / 2);
            }));
        }
    
        protected override void DestroySelf(float time)
        {
            m_HitFx.DoColor(new Color(1, 1, 1, 0), 0.2f).SetDelay(time);
            Destroy(gameObject,time + 0.2f);
        }
    }
}