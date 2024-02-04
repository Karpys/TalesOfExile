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
            if (targetTransform == null)
                targetTransform = MapData.Instance.transform;

            return Instantiate(this,position,Quaternion.identity,targetTransform);;
        }

        protected virtual float GetAnimationDuration()
        {
            return m_AnimationLockTime;
        }
        protected abstract void Animate();
    }
}