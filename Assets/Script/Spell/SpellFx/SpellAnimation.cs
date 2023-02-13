using System;
using UnityEngine;

public abstract class SpellAnimation : MonoBehaviour
{
    public float BaseSpellDelay => GetAnimationDuration();

    

    public void TriggerFx(Vector3 position,Transform targetEntity = null)
    {
        if (targetEntity)
        {
            GameObject obj = Instantiate(gameObject, targetEntity);
            obj.transform.localPosition = position;
            return;   
        }

        Instantiate(gameObject, position, Quaternion.identity);
    }

    protected abstract float GetAnimationDuration();
    public abstract void Animate();
    public abstract void DestroySelf(float time);
}

public class BurstAnimation : SpellAnimation
{
    private void Awake()
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