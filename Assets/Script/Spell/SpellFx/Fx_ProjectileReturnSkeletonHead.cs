using UnityEngine;

namespace KarpysDev.Script.Spell.SpellFx
{
    public class Fx_ProjectileReturnSkeletonHead : Fx_ProjectileReturn
    {
        [SerializeField] private Color m_ReturnColor = Color.green;

        protected override void Return()
        {
            m_Visual.color = m_ReturnColor;
            base.Return();
        }
    }
}