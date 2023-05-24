using System.Collections;
using UnityEngine;

namespace KarpysDev.Script.Spell.SpellFx
{
    public abstract class BurstAnimation : SpellAnimation
    {
        [SerializeField] protected float m_SpellAnimFixeDelay = 0;
        protected virtual void Start()
        {
            if (m_SpellAnimFixeDelay <= 0)
            {
                Animate();
                DestroySelf(GetAnimationDuration());
            }
            else
            {
                StartCoroutine(DelayAnim());
                IEnumerator DelayAnim()
                {
                    yield return new WaitForSeconds(m_SpellAnimFixeDelay);
                    Animate();
                    DestroySelf(GetAnimationDuration());
                }
            }
        }
        protected override float GetAnimationDuration()
        {
            return m_SpellAnimFixeDelay;
        }

        protected override void Animate() {}

        protected abstract void DestroySelf(float time);

    }
}