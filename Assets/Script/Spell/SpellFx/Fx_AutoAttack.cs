using TweenCustom;
using UnityEngine;

public class Fx_AutoAttack : BurstAnimation
{
    [SerializeField] private float m_AnimDuration = 0.1f;
    [SerializeField] private SpriteRenderer m_HitFx = null;
    
    protected override float GetAnimationDuration()
    {
        return m_AnimDuration;
    }

    protected override void Animate()
    {
        base.Animate();
        SpellData spellData = m_Datas[0] as SpellData;
        BoardEntity entity = m_Datas[1] as BoardEntity;

        Vector3 targetPosition = (entity.WorldPosition + spellData.AttachedEntity.WorldPosition) / 2;
        spellData.AttachedEntity.VisualTransform.DoMove(targetPosition, m_AnimDuration / 2).OnComplete((() =>
        {
            m_HitFx.gameObject.SetActive(true);
            spellData.AttachedEntity.VisualTransform.DoMove(spellData.AttachedEntity.WorldPosition, m_AnimDuration / 2);
        }));
    }
    
    protected override void DestroySelf(float time)
    {
        m_HitFx.DoColor(new Color(1, 1, 1, 0), 0.2f).SetDelay(time);
        Destroy(gameObject,time + 0.2f);
    }
}