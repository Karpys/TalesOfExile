using System.Collections;
using UnityEngine;

namespace KarpysDev.Script.Spell.SpellFx
{
    public class Fx_DelayedAnimation : Fx_BurstAnimation
    {
        [SerializeField] private SpellAnimation m_Animation = null;
        [SerializeField] private float m_DelayedTime = 0;
        protected override void Animate()
        {
            StartCoroutine(TriggerDelay());
            
            IEnumerator TriggerDelay()
            {
                yield return new WaitForSeconds(m_DelayedTime);
                m_Animation.TriggerFx(transform.position, null, m_Datas);
                Destroy(gameObject);
            }
        }
    }
}