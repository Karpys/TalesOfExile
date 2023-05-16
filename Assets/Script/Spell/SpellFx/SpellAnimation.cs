using System;
using UnityEngine;

public abstract class SpellAnimation : MonoBehaviour
{
    public float BaseSpellDelay => GetAnimationDuration();
    protected object[] m_Datas = null;
    
    public SpellAnimation TriggerFx(Vector3 position,Transform transform = null,params object[] args)
    {
        SpellAnimation anim = null;

        if (transform == null)
            transform = MapData.Instance.transform;
        
        anim = Instantiate(this,position,Quaternion.identity,transform);
        
        anim.SetArgs(args);

        return anim;
    }

    private void SetArgs(object[] args)
    {
        m_Datas = args;
    }

    protected abstract float GetAnimationDuration();
    protected abstract void Animate();
}