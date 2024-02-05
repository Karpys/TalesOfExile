using System.Collections;
using UnityEngine;

namespace KarpysDev.Script.Spell.SpellFx
{
    public class FxDelayedAnimation : FxBurstAnimation
    {
        [SerializeField] private SpellAnimation m_Animation = null;
        [SerializeField] private float m_DelayedTime = 0;
        protected override void Animate()
        {
            StartCoroutine(TriggerDelay());
            
            IEnumerator TriggerDelay()
            {
                yield return new WaitForSeconds(m_DelayedTime);
                m_Animation.TriggerFx(transform.position, null);
                Destroy(gameObject);
            }
        }
    }
}