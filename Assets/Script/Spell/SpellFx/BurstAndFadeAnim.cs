 using TweenCustom;
using UnityEngine;

public class BurstAndFadeAnim : BurstAnimation
{
    [SerializeField] protected SpriteRenderer m_Sprite = null;
    [SerializeField] protected float m_FadeDuration = 0.2f;
    [SerializeField] protected float m_AnimDuration = 0.2f;


    protected override void Animate()
    {
        base.Animate();
        m_Sprite.gameObject.SetActive(true);
    }

    protected override float GetAnimationDuration()
    {
        return m_AnimDuration;
    }
    
    protected override void DestroySelf(float time)
    {
        m_Sprite.DoColor(new Color(1, 1, 1, 0), m_FadeDuration);
        Destroy(gameObject,m_FadeDuration + 0.1f);
    }
}