using System;
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
            anim = Instantiate(this,position,Quaternion.identity,targetEntity);
        }
        else
        {
            anim = Instantiate(this, position, Quaternion.identity);
        }
        
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