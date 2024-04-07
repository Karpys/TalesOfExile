namespace KarpysDev.Script.Spell.SpellFx
{
    using UnityEngine;

    public abstract class FxBurstAnimation : SpellAnimation
    {
        protected virtual void Start()
        {
            Animate();
        }
    }

    public class FxPalmClap : FxBurstAnimation,ISizeable
    {
        [SerializeField] private Transform m_RightPalm = null;
        [SerializeField] private Transform m_LeftPalm = null;
        private int m_Size = 0;
        protected override void Animate()
        {
            
        }

        public void SetSize(int size)
        {
            m_Size = size;
        }
    }
}