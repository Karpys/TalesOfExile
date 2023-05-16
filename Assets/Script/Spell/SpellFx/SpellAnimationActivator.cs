using UnityEngine;

public class SpellAnimationActivator : SpellAnimation
{
    [SerializeField] private float m_AnimDuration = 0;
    [SerializeField] private SpellAnimation m_ActivatorAnimation = null;
    protected override float GetAnimationDuration()
    {
        return m_AnimDuration;
    }

    public void Activate()
    {
        Animate();
    }

    protected override void Animate()
    {
        m_ActivatorAnimation.TriggerFx(transform.position);
        Destroy(gameObject);
    }
}