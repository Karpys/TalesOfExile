using System;
using KarpysDev.Script.Map_Related;
using UnityEngine;

namespace KarpysDev.Script.Spell.SpellFx
{
    public abstract class SpellAnimation : MonoBehaviour
    {
        [SerializeField] protected float m_AnimationLockTime = 0.2f;
        public float BaseSpellDelay => GetAnimationDuration();
        protected object[] m_Datas = null;
    
        public SpellAnimation TriggerFx(Vector3 position,Transform targetTransform = null,params object[] args)
        {
            SpellAnimation anim = null;

            if (targetTransform == null)
                targetTransform = MapData.Instance.transform;
        
            anim = Instantiate(this,position,Quaternion.identity,targetTransform);
        
            anim.SetArgs(args);

            return anim;
        }

        private void SetArgs(object[] args)
        {
            m_Datas = args;
        }

        protected virtual float GetAnimationDuration()
        {
            return m_AnimationLockTime;
        }
        protected abstract void Animate();
    }
}