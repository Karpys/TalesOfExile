using System.Collections.Generic;
using KarpysDev.Script.Utils;
using TweenCustom;
using UnityEngine;

namespace KarpysDev.Script.Spell.SpellFx
{
    public class Fx_SplitProjectile : SpellAnimation
    {
        [SerializeField] private SpriteRenderer m_Visual = null;
        [SerializeField] private SpellAnimation m_ProjectileSplitAnimation = null;
        [SerializeField] private Vector2 ProjectileSpeedReference = new Vector2(5, 0.2f);
       
        private List<Vector3> m_Points = new List<Vector3>();

        protected void Start()
        {
            if (m_Datas.Length == 0)
            {
                Debug.LogError("Try Launch Fx with no start / end position data");
                return;
            }
        
            transform.position = (Vector3)m_Datas[0];
            m_Points = (List<Vector3>)m_Datas[1];

            Animate();
        }
        
        protected override float GetAnimationDuration()
        {
            return ProjectileSpeedReference.y;
        }

        protected override void Animate()
        {
            Vector3 position = transform.position; 
            float arrowSpeed = Vector3.Distance(position, m_Points[0]) * ProjectileSpeedReference.y / ProjectileSpeedReference.x;
            SpriteUtils.RotateTowardPoint(position, m_Points[0], m_Visual.transform);
            transform.DoMove(m_Points[0], arrowSpeed).OnComplete(CreateSplit);
        }

        private void CreateSplit()
        {
            for (int i = 1; i < m_Points.Count; i++)
            {
                m_ProjectileSplitAnimation.TriggerFx(m_Points[0], null, m_Points[0], m_Points[i]);
            }
            
            Destroy(gameObject);
        }
    }
}