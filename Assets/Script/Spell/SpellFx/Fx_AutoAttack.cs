using KarpysDev.Script.Utils;
using TweenCustom;
using UnityEngine;

namespace KarpysDev.Script.Spell.SpellFx
{
    public class Fx_AutoAttack : BurstAnimation
    {
        [SerializeField] private float m_AnimDuration = 0.1f;
        [SerializeField] private SpriteRenderer m_HitFx = null;
    
        protected override void Animate()
        {
            Vector3 startPosition = (Vector3)m_Datas[0];
            Vector3 endPosition = (Vector3)m_Datas[1];
            Transform targetTransform = (Transform)m_Datas[2];

            Vector3 targetPosition = (endPosition + startPosition) / 2;
            targetTransform.DoMove(targetPosition, m_AnimDuration / 2).OnComplete(() =>
            {
                DisplayFx();
                if(targetTransform)
                    targetTransform.DoMove(startPosition, m_AnimDuration / 2); 
            }).OnReferenceLose(DisplayFx);
        }

        private void DisplayFx()
        {
            m_HitFx.gameObject.SetActive(true);
            m_HitFx.FadeAndDestroy(new Color(1,1,1,0),0.2f + m_AnimDuration/2,gameObject);
        }
    }
}