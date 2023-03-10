 using TweenCustom;
using UnityEngine;

public class BurstAndFadeAnim : BurstAnimation
{
    [SerializeField] private SpriteRenderer m_Sprite = null;
    [SerializeField] private float m_FadeDuration = 0.2f;
    [SerializeField] private float m_AnimDuration = 0.2f;


    public override void Animate()
    {
        base.Animate();
        m_Sprite.gameObject.SetActive(true);
    }

    protected override float GetAnimationDuration()
    {
        return m_AnimDuration;
    }
    
    public override void DestroySelf(float time)
    {
        m_Sprite.DoColor(new Color(1, 1, 1, 0), m_FadeDuration);
        Destroy(gameObject,m_FadeDuration + 0.1f);
    }
}