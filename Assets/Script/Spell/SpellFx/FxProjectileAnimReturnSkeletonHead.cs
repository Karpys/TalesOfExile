using UnityEngine;

namespace KarpysDev.Script.Spell.SpellFx
{
    public class FxProjectileAnimReturnSkeletonHead : FxProjectileAnimReturn
    {
        [SerializeField] private Color m_ReturnColor = Color.green;

        protected override void Return()
        {
            m_Visual.color = m_ReturnColor;
            base.Return();
        }
    }
}