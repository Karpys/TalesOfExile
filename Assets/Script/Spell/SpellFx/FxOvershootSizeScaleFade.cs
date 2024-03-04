using KarpysDev.Script.Utils;
using UnityEngine;

namespace KarpysDev.Script.Spell.SpellFx
{
    public class FxOvershootSizeScaleFade : FxSizeScaleFade
    {
        [SerializeField] private float m_OvershootValue = 2;
        public override void SetSize(int size)
        {
            base.SetSize(size);
            m_TweenData.TargetTransform.localScale = Vector3.one * (size.ToGameSize() * m_OvershootValue);
        }
    }
}