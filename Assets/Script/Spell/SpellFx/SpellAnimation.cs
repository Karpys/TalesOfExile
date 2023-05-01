using System;
using System.Collections;
using UnityEngine;

public abstract class SpellAnimation : MonoBehaviour
{
    public float BaseSpellDelay => GetAnimationDuration();
    protected object[] m_Datas = null;
    
    public SpellAnimation TriggerFx(Vector3 position,Transform targetEntity = null,object[] args = null)
    {
        SpellAnimation anim = null;

        if (targetEntity)
        {
            anim = Instantiate(this, targetEntity);
        }
        else
        {
            anim = Instantiate(this, position, Quaternion.identity);
        }
        
        anim.transform.localPosition = position;
        anim.SetArgs(args);

        return anim;
    }

    private void SetArgs(object[] args)
    {
        m_Datas = args;
    }

    protected abstract float GetAnimationDuration();
    protected abstract void Animate();
    protected abstract void DestroySelf(float time);
}

public class BurstAnimation : SpellAnimation
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

    protected override void DestroySelf(float time)
    {
        Destroy(gameObject,time);
    }
}