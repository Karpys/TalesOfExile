using System;
using UnityEngine;

public abstract class SpellAnimation : MonoBehaviour
{
    public float BaseSpellDelay => GetAnimationDuration();
    protected object[] m_Datas = null;
    
    public void TriggerFx(Vector3 position,Transform targetEntity = null,object[] args = null)
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
    }

    private void SetArgs(object[] args)
    {
        m_Datas = args;
    }

    protected abstract float GetAnimationDuration();
    public abstract void Animate();
    public abstract void DestroySelf(float time);
}

public class BurstAnimation : SpellAnimation
{
    private void Start()
    {
        Animate();
        DestroySelf(GetAnimationDuration());
    }
    protected override float GetAnimationDuration()
    {
        return 0;
    }

    public override void Animate() {}

    public override void DestroySelf(float time)
    {
        Destroy(gameObject,time);
    }
}