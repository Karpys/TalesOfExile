using System.Collections;
using System.Collections.Generic;
using TweenCustom;
using UnityEngine;

public class Fx_ProjectileSequence : SpellAnimation
{
    [SerializeField] private SpriteRenderer m_Visual = null;
    [SerializeField] private Vector2 ProjectileSpeedReference = new Vector2(5, 0.2f);

    private List<Vector3> points = new List<Vector3>();
    
    protected void Start()
    {
        if (m_Datas.Length == 0)
        {
            Debug.LogError("Try Launch Fx with no start / end position data");
            return;
        }
        
        transform.position = (Vector3)m_Datas[0];
        points = (List<Vector3>)m_Datas[1];

        Animate();
    }

    protected override float GetAnimationDuration()
    {
        return ProjectileSpeedReference.y;
    }

    protected override void Animate()
    {
        bool isLast = false;
        StartCoroutine(CO_Sequence());
        
        IEnumerator CO_Sequence()
        {
            for (int i = 0; i < points.Count; i++)
            {
                if (i == points.Count - 1)
                    isLast = true;
            
                float arrowSpeed = Vector3.Distance(transform.position, points[i]) * ProjectileSpeedReference.y / ProjectileSpeedReference.x;
                SpriteUtils.RotateTowardPoint(transform.position, points[i], m_Visual.transform);
                
                if (isLast)
                {
                    transform.DoMove(points[i], arrowSpeed).OnComplete(() => Destroy(gameObject));
                }
                else
                {
                    transform.DoMove(points[i], arrowSpeed);
                }
                
                yield return new WaitForSeconds(arrowSpeed);
            }
        }
    }
}