﻿using TweenCustom;
using UnityEngine;

namespace KarpysDev.Script.Spell.SpellFx
{
    public class Fx_CurseSpread : BurstAndFade
    {
        [Header("Rotation Param")]
        [SerializeField] private float m_RotationDelay = 0;
        [SerializeField] private float m_RotationStrenght = 0;
        [SerializeField] private float m_RotationDuration = 0;
        protected override void Animate()
        {
            transform.DoRotate(new Vector3(0, 0, m_RotationStrenght), m_RotationDuration).SetDelay(m_RotationDelay)
                .OnComplete(
                    () =>
                    {
                        base.Animate();
                    });
        }
    }
}