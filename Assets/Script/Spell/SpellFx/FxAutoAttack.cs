using KarpysDev.Script.Utils;
using UnityEngine;

namespace KarpysDev.Script.Spell.SpellFx
{
    using KarpysUtils.TweenCustom;

    public class FxAutoAttack : FxBurstAnimation,IYoYoTransform
    {
        [SerializeField] private float m_AnimDuration = 0.1f;
        [SerializeField] private SpriteRenderer m_HitFx = null;
    
        private Vector3 m_InitialPos = Vector3.zero;
        private Vector3 m_GoToPosition = Vector3.zero;
        private Transform m_TransformToMove = null;
        
        public Vector3 InitialPosition { set => m_InitialPos = value; }
        public Vector3 GoToPosition { set => m_GoToPosition = value; }
        public Transform TransformToMove { set => m_TransformToMove = value;}
        protected override void Animate()
        {
            Vector3 targetPosition = (m_GoToPosition + m_InitialPos) / 2;
            m_TransformToMove.DoMove(targetPosition, m_AnimDuration / 2).OnComplete(() =>
            {
                DisplayFx();
                if(m_TransformToMove)
                    m_TransformToMove.DoMove(m_InitialPos, m_AnimDuration / 2); 
            }).OnReferenceLose(DisplayFx);
        }

        private void DisplayFx()
        {
            m_HitFx.gameObject.SetActive(true);
            m_HitFx.FadeAndDestroy(new Color(1,1,1,0),0.2f + m_AnimDuration/2,gameObject);
        }
    }

    public interface IYoYoTransform
    {
        Vector3 InitialPosition { set; }
        Transform TransformToMove{ set; }
        Vector3 GoToPosition { set; }
    }
}