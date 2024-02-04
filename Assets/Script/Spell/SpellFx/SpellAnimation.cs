using System;
using KarpysDev.Script.Map_Related;
using UnityEngine;

namespace KarpysDev.Script.Spell.SpellFx
{
    public abstract class SpellAnimation : MonoBehaviour
    {
        [SerializeField] protected float m_AnimationLockTime = 0.2f;
        public float BaseSpellDelay => GetAnimationDuration();

        public SpellAnimation TriggerFx(Vector3 position,Transform targetTransform = null)
        {
            SpellAnimation anim = null;

            if (targetTransform == null)
                targetTransform = MapData.Instance.transform;
        
            //To Optimize: Create a pool Fx Manager, not necessary for the moment
            anim = Instantiate(this,position,Quaternion.identity,targetTransform);
        
            /*anim.SetArgs(args);*/

            return anim;
        }

        protected virtual float GetAnimationDuration()
        {
            return m_AnimationLockTime;
        }
        protected abstract void Animate();
    }
}