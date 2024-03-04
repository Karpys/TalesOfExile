using KarpysDev.Script.Utils;

namespace KarpysDev.Script.Spell.SpellFx
{
    public class FxSizeScaleFade : FxScaleFade,ISizeable
    {
        public virtual void SetSize(int size)
        {
            m_TweenData.EndValue *= size.ToGameSize();
        }
    }
}