using TweenCustom;
using UnityEngine;

public class Fx_Projectile : BurstAnimation
{
    [SerializeField] private SpriteRenderer m_Visual = null;

    //[CONST]//
    private Vector2 MAXDISTANCE_REFERENCE = new Vector2(5, 0.2f);
    
    private Vector3 m_StartPosition = Vector3.zero;
    private Vector3 m_EndPosition = Vector3.zero;

    protected override float GetAnimationDuration()
    {
        return MAXDISTANCE_REFERENCE.y;
    }

    protected override void Start()
    {
        if (m_Datas.Length == 0)
        {
            Debug.LogError("Try Launch Fx with no start / end position data");
            return;
        }
        
        m_StartPosition = (Vector3)m_Datas[0];
        m_EndPosition = (Vector3)m_Datas[1];
        
        transform.position = m_StartPosition;
        SpriteUtils.RotateTowardPoint(m_StartPosition, m_EndPosition, m_Visual.transform);
        m_Visual.enabled = true;
        
        base.Start();
    }

    
    
    protected override void Animate()
    {
        base.Animate();

        float arrowSpeed = Vector3.Distance(transform.position, m_EndPosition) * MAXDISTANCE_REFERENCE.y / MAXDISTANCE_REFERENCE.x;
        transform.DoMove(m_EndPosition, arrowSpeed).OnComplete(() => Destroy(gameObject));
    }

    protected override void DestroySelf(float time)
    {
        //base.DestroySelf(0.2f);
    }
}