using TweenCustom;
using UnityEngine;

public class Fx_ArrowAnim : BurstAnimation
{
    [SerializeField] private SpriteRenderer m_Visual = null;

    //[CONST]//
    private Vector2 MAXDISTANCE_REFERENCE = new Vector2(5, 0.2f);

    protected override float GetAnimationDuration()
    {
        return MAXDISTANCE_REFERENCE.y;
    }

    protected override void Start()
    {
        transform.position = (m_Datas[0] as SpellData).AttachedEntity.WorldPosition;
        RotateTowardPoint((m_Datas[1] as BoardEntity).WorldPosition);
        m_Visual.enabled = true;
        base.Start();
    }

    private void RotateTowardPoint(Vector3 point)
    {
        Vector3 direction = point - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        
        m_Visual.transform.rotation = Quaternion.Euler(new Vector3(0,0,angle));
    }
    
    protected override void Animate()
    {
        base.Animate();
        //BoardEntity caster = (m_Datas[0] as SpellData).AttachedEntity;
        BoardEntity entity = m_Datas[1] as BoardEntity;

        float arrowSpeed = Vector3.Distance(transform.position, entity.WorldPosition) * MAXDISTANCE_REFERENCE.y / MAXDISTANCE_REFERENCE.x;
        transform.DoMove(entity.WorldPosition, arrowSpeed).OnComplete(() => Destroy(gameObject));
    }

    protected override void DestroySelf(float time)
    {
        base.DestroySelf(0.2f);
    }
}